﻿using Microsoft.Deployment.Compression.Cab;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FixPluginTypesSerialization.Util
{
    internal class MiniPdbReader
    {
        private readonly PeReader _peReader;

        private byte[] _pdbFile;

        internal bool IsPdbAvailable;

        internal bool UseCache;

        internal MiniPdbReader(string targetFilePath)
        {
            _peReader = new PeReader(targetFilePath);

            if (_peReader.RsdsPdbFileName == null)
            {
                Log.Info("No pdb path found in the pe file. Falling back to supported versions");
            }
            else
            {
                UseCache = Config.LastDownloadedGUID.Value == _peReader.PdbGuid;

                Log.Message($"{(UseCache ? "U" : "Not u")}sing the config cache");

                if (!UseCache)
                {
                    if (DownloadUnityPdb(_peReader))
                    {
                        Config.LastDownloadedGUID.Value = _peReader.PdbGuid;

                        IsPdbAvailable = true;
                    }
                    else
                    {
                        Log.Info("Failed to find the linked pdb in the unity symbol server. Falling back to supported versions");
                    }
                }
                else
                {
                    IsPdbAvailable = true;
                }
            }
        }

        private bool DownloadUnityPdb(PeReader peReader)
        {
            var pdbCompressedPath = peReader.RsdsPdbFileName.TrimEnd('b') + '_';
            var pdbDownloadUrl = $"{peReader.RsdsPdbFileName}/{peReader.PdbGuid}/{pdbCompressedPath}";

            var tempPath = Path.GetTempPath();
            var pdbCabPath = Path.Combine(tempPath, "pdb.cab");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PdbDownload.exe"),
                    Arguments = $"\"{pdbDownloadUrl}\" \"{pdbCabPath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                },
            };

            process.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Log.Info(e.Data);
                }
            };
            process.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Log.Error(e.Data);
                }
            };

            var start = process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (File.Exists(pdbCabPath))
            {
                var cabInfo = new CabInfo(pdbCabPath);

                Log.Info("Unpacking the compressed pdb");
                cabInfo.Unpack(tempPath);

                var pdbPath = Path.Combine(tempPath, peReader.RsdsPdbFileName);

                _pdbFile = File.ReadAllBytes(pdbPath);

                File.Delete(pdbCabPath);
                File.Delete(pdbPath);
            }

            return _pdbFile != null;
        }

        internal unsafe IntPtr FindFunctionOffset(BytePattern[] bytePatterns)
        {
            fixed (byte* pdbFileStartPtr = &_pdbFile[0])
            {
                IntPtr pdbStartAddress = (IntPtr)pdbFileStartPtr;
                long sizeOfPdb = _pdbFile.Length;

                var match = bytePatterns.Select(p => new { p, res = p.Match(pdbStartAddress, sizeOfPdb) })
                .FirstOrDefault(m => m.res > 0);
                if (match == null)
                {
                    return IntPtr.Zero;
                }

                Log.Info($"Found at {match.res:X} ({pdbStartAddress.ToInt64() + match.res:X})");

                var functionOffsetPtr = (uint*)(pdbStartAddress.ToInt64() + match.res - 7);
                var functionOffset = *functionOffsetPtr;

                var sectionIndexPtr = (ushort*)(pdbStartAddress.ToInt64() + match.res - 3);
                var sectionIndex = *sectionIndexPtr - 1;

                functionOffset += _peReader.ImageSectionHeaders[sectionIndex].VirtualAddress;

                Log.Info("Function offset : " + functionOffset.ToString("X") + " | PE section : " + sectionIndex);

                return new IntPtr(functionOffset);
            }
        }
    }
}

using Microsoft.Deployment.Compression.Cab;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace FixPluginTypesSerialization.Util
{
    internal class MiniPdbReader
    {
        private static readonly HttpClient _httpClient = new();

        private readonly PeReader _peReader;

        private byte[] _pdbFile;

        internal bool UseCache;

        private static byte[] DownloadFromWeb(string url)
        {
            Log.LogInfo("Downloading : " + url);
            
            var httpResponse = _httpClient.GetAsync(url).GetAwaiter().GetResult();

            Log.LogInfo("Status Code : " + httpResponse.StatusCode);

            if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            return httpResponse.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }

        internal MiniPdbReader(string targetFilePath)
        {
            _peReader = new PeReader(targetFilePath);

            if (_peReader.RsdsPdbFileName == null)
            {
                Log.LogInfo("No pdb path found in the pe file");
                return;
            }

            UseCache = Config.LastDownloadedGUID.Value == _peReader.PdbGuid;

            Log.LogMessage($"{(UseCache ? "U" : "Not u")}sing the config cache.");

            if (!UseCache)
            {
                DownloadUnityPdb(_peReader);

                if (_pdbFile != null)
                {
                    Config.LastDownloadedGUID.Value = _peReader.PdbGuid;
                }
            }
        }

        private void DownloadUnityPdb(PeReader peReader)
        {
            const string unitySymbolServer = "http://symbolserver.unity3d.com/";

            var pdbCompressedPath = peReader.RsdsPdbFileName.TrimEnd('b') + '_';
            var pdbDownloadUrl = Path.Combine(unitySymbolServer, peReader.RsdsPdbFileName, peReader.PdbGuid, pdbCompressedPath);

            var compressedPdbCab = DownloadFromWeb(pdbDownloadUrl);

            if (compressedPdbCab != null)
            {
                var tempPath = Path.GetTempPath();

                var pdbCabPath = Path.Combine(tempPath, "pdb.cab");

                Log.LogInfo("Writing the compressed pdb to " + pdbCabPath);
                File.WriteAllBytes(pdbCabPath, compressedPdbCab);

                var cabInfo = new CabInfo(pdbCabPath);

                Log.LogInfo("Unpacking the compressed pdb");
                cabInfo.Unpack(tempPath);

                var pdbPath = Path.Combine(tempPath, peReader.RsdsPdbFileName);

                _pdbFile = File.ReadAllBytes(pdbPath);

                File.Delete(pdbCabPath);
                File.Delete(pdbPath);
            }
        }

        internal unsafe IntPtr FindFunctionOffset(BytePattern[] bytePatterns)
        {
            fixed (byte* pdbFileStartPtr = &_pdbFile[0])
            {
                IntPtr pdbStartAddress = (IntPtr)pdbFileStartPtr;
                long sizeOfPdb = _pdbFile.Length;
                long pdbEndAddress = (long)(pdbFileStartPtr + sizeOfPdb);

                var match = bytePatterns.Select(p => new { p, res = p.Match(pdbStartAddress, pdbEndAddress) })
                .FirstOrDefault(m => m.res >= 0);
                if (match == null)
                {
                    Log.LogError("No match found, cannot hook ! Please report it to the r2api devs !");
                    return IntPtr.Zero;
                }

                Log.LogInfo($"Found at {match.res:X} ({pdbStartAddress.ToInt64() + match.res:X})");

                var functionOffsetPtr = (uint*)(pdbStartAddress.ToInt64() + match.res - 7);
                var functionOffset = *functionOffsetPtr;

                var sectionIndexPtr = (ushort*)(pdbStartAddress.ToInt64() + match.res - 3);
                var sectionIndex = *sectionIndexPtr - 1;

                functionOffset += _peReader.ImageSectionHeaders[sectionIndex].VirtualAddress;

                Log.LogInfo("Function offset : " + functionOffset.ToString("X") + " | PE section : " + sectionIndex);

                return new IntPtr(functionOffset);
            }
        }
    }
}

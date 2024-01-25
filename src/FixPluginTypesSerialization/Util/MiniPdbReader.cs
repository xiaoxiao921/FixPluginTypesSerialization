using Microsoft.Deployment.Compression.Cab;
using System;
using System.IO;
using System.Linq;
using System.Net;
#if NETSTANDARD2_0_OR_GREATER
using System.Net.Http;
using System.Threading.Tasks;
#endif

namespace FixPluginTypesSerialization.Util
{
    internal class MiniPdbReader
    {
#if NETSTANDARD2_0_OR_GREATER
        private static readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromMinutes(5) };
#else
        private static readonly WebClientWithTimeout _webClient = new();
#endif
        private readonly PeReader _peReader;

        private byte[] _pdbFile;

        internal bool IsPdbAvailable;

        internal bool UseCache;

        private static byte[] DownloadFromWeb(string url)
        {
            Log.Info("Downloading : " + url + "\nThis pdb file is needed for the plugin to work properly. This may take a while, relax, modding is coming.");
#if NETSTANDARD2_0_OR_GREATER
            try
            {

                var httpResponse = _httpClient.GetAsync(url).GetAwaiter().GetResult();

                Log.Info("Status Code : " + httpResponse.StatusCode);

                if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }

                return httpResponse.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            }
            catch (TaskCanceledException)
            {
                Log.Info("Could not download pdb. Plugin may not work correctly.");
                return null;
            }
#else
            try
            {
                return _webClient.DownloadData(url);
        }
            catch (WebException)
            {
                Log.Info("Could not download pdb. Plugin may not work correctly.");
                return null;
            }
#endif
        }

        internal MiniPdbReader(string targetFilePath)
        {
            _peReader = new PeReader(targetFilePath);

            if (_peReader.RsdsPdbFileName == null)
            {
                Log.Info("No pdb path found in the pe file. Falling back to sig matching");
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
                        Log.Info("Failed to find the linked pdb in the unity symbol server. Falling back to sig matching");
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
            const string unitySymbolServer = "http://symbolserver.unity3d.com";

            var pdbCompressedPath = peReader.RsdsPdbFileName.TrimEnd('b') + '_';
            var pdbDownloadUrl = $"{unitySymbolServer}/{peReader.RsdsPdbFileName}/{peReader.PdbGuid}/{pdbCompressedPath}";

            var compressedPdbCab = DownloadFromWeb(pdbDownloadUrl);

            if (compressedPdbCab != null)
            {
                var tempPath = Path.GetTempPath();

                var pdbCabPath = Path.Combine(tempPath, "pdb.cab");

                try
                {
                    File.Delete(pdbCabPath);
                }
                catch (Exception)
                {
                }

                Log.Info("Writing the compressed pdb to " + pdbCabPath);
                File.WriteAllBytes(pdbCabPath, compressedPdbCab);

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

#if NET35 || NET40
        private class WebClientWithTimeout : WebClient
        {
            public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                request.Timeout = (int)Timeout.TotalMilliseconds;
                return request;
            }
        }
#endif
    }
}

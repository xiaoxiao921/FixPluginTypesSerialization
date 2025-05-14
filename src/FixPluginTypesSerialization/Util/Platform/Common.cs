using System;

#if NETSTANDARD
using System.Runtime.InteropServices;
#endif

namespace FixPluginTypesSerialization.Util.Platform;

public static class Common
{
    public static bool DownloadBytes(string url, out byte[] data)
    {
#if NETSTANDARD
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Win32.DownloadFile(url, out data);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return Unix.DownloadFile(url, out data);

        throw new NotSupportedException(RuntimeInformation.OSDescription + " is not supported");
#elif NET40 || NET35
        var platform = Environment.OSVersion.Platform;

        if (platform is PlatformID.Win32NT or PlatformID.Win32S or PlatformID.Win32Windows or PlatformID.WinCE)
            return Win32.DownloadFile(url, out data);

        if (platform is PlatformID.Unix or PlatformID.MacOSX)
            return Unix.DownloadFile(url, out data);

        throw new NotSupportedException($"{platform} is not supported");
#endif
    }
}
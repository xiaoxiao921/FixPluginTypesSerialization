using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.Util.Platform;

public static class Win32
{
    private const string WinINet = "wininet.dll";
    
    [DllImport(WinINet, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr InternetOpen(
        string lpszAgent,
        uint dwAccessType,
        string? lpszProxyName,
        string? lpszProxyBypass,
        uint dwFlags
    );

    [DllImport(WinINet, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr InternetOpenUrl(
        IntPtr hInternet,
        string lpszUrl,
        string? lpszHeaders,
        uint dwHeadersLength,
        uint dwFlags,
        uint dwContext
    );

    [DllImport(WinINet, SetLastError = true)]
    private static extern bool InternetReadFile(
        IntPtr hFile,
        byte[] lpBuffer,
        uint dwNumberOfBytesToRead,
        out uint lpNumberOfBytesRead
    );

    [DllImport(WinINet, SetLastError = true)]
    private static extern bool InternetCloseHandle(IntPtr hInternet);

    private const uint INTERNET_OPEN_TYPE_DIRECT = 1;
    private const uint INTERNET_FLAG_RELOAD = 0x80000000;

    public static bool DownloadFile(string url, out byte[] data)
    {
        data = [];
        
        var hInternet = InternetOpen("FixPluginTypeSerialization", INTERNET_OPEN_TYPE_DIRECT, null, null, 0);
        if (hInternet == IntPtr.Zero)
            return false;

        var hConnect = InternetOpenUrl(hInternet, url, null, 0, INTERNET_FLAG_RELOAD, 0);
        if (hConnect == IntPtr.Zero)
        {
            InternetCloseHandle(hInternet);
            return false;
        }

        using var ms = new MemoryStream();
        var buffer = new byte[8192];

        while (InternetReadFile(hConnect, buffer, (uint)buffer.Length, out var bytesRead) && bytesRead > 0)
            ms.Write(buffer, 0, (int)bytesRead);

        InternetCloseHandle(hConnect);
        InternetCloseHandle(hInternet);

        data = ms.ToArray();

        return true;
    }
}
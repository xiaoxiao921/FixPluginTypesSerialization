using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.Util.Platform;

public static class Win32
{
    private const int INTERNET_DEFAULT_HTTPS_PORT = 443;
    private const int INTERNET_DEFAULT_HTTP_PORT = 80;
    private const int WINHTTP_FLAG_SECURE = 0x00800000;

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern IntPtr WinHttpOpen(
        [MarshalAs(UnmanagedType.LPWStr)] string userAgent,
        int accessType,
        IntPtr proxyName,
        IntPtr proxyBypass,
        int flags);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern IntPtr WinHttpConnect(
        IntPtr session,
        [MarshalAs(UnmanagedType.LPWStr)] string serverName,
        ushort serverPort,
        int reserved);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern IntPtr WinHttpOpenRequest(
        IntPtr connect,
        [MarshalAs(UnmanagedType.LPWStr)] string verb,
        [MarshalAs(UnmanagedType.LPWStr)] string objectName,
        [MarshalAs(UnmanagedType.LPWStr)] string version,
        IntPtr referrer,
        IntPtr acceptTypes,
        int flags);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern bool WinHttpSendRequest(
        IntPtr request,
        IntPtr headers,
        int headersLength,
        IntPtr optional,
        int optionalLength,
        int totalLength,
        IntPtr context);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern bool WinHttpReceiveResponse(IntPtr request, IntPtr reserved);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern bool WinHttpQueryDataAvailable(IntPtr request, out int bytesAvailable);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern bool WinHttpReadData(
        IntPtr request,
        byte[] buffer,
        int bufferSize,
        out int bytesRead);

    [DllImport("winhttp.dll", SetLastError = true)]
    private static extern bool WinHttpCloseHandle(IntPtr handle);

    public static bool DownloadFile(string url, string filename)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        var secure = uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
        var port = uri.Port != -1 ? uri.Port : secure ? INTERNET_DEFAULT_HTTPS_PORT : INTERNET_DEFAULT_HTTP_PORT;
        var flags = secure ? WINHTTP_FLAG_SECURE : 0;

        var hSession = WinHttpOpen("FixPluginTypesSerialization", 0, IntPtr.Zero, IntPtr.Zero, 0);
        if (hSession == IntPtr.Zero)
            return false;

        var hConnect = WinHttpConnect(hSession, uri.Host, (ushort)port, 0);
        if (hConnect == IntPtr.Zero)
        {
            WinHttpCloseHandle(hSession);
            return false;
        }

        var path = string.IsNullOrEmpty(uri.PathAndQuery) ? "/" : uri.PathAndQuery;
        var hRequest = WinHttpOpenRequest(hConnect, "GET", path, null, IntPtr.Zero, IntPtr.Zero, flags);
        if (hRequest == IntPtr.Zero)
        {
            WinHttpCloseHandle(hConnect);
            WinHttpCloseHandle(hSession);
            return false;
        }

        if (!WinHttpSendRequest(hRequest, IntPtr.Zero, 0, IntPtr.Zero, 0, 0, IntPtr.Zero) ||
            !WinHttpReceiveResponse(hRequest, IntPtr.Zero))
        {
            WinHttpCloseHandle(hRequest);
            WinHttpCloseHandle(hConnect);
            WinHttpCloseHandle(hSession);
            return false;
        }

        using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
        var buffer = new byte[8192];

        while (true)
        {
            if (!WinHttpQueryDataAvailable(hRequest, out var size) || size == 0)
                break;

            var totalRead = 0;
            while (totalRead < size)
            {
                var toRead = Math.Min(buffer.Length, size - totalRead);
                if (!WinHttpReadData(hRequest, buffer, toRead, out var bytesRead) || bytesRead == 0)
                    break;

                fs.Write(buffer, 0, bytesRead);
                totalRead += bytesRead;
            }
        }

        WinHttpCloseHandle(hRequest);
        WinHttpCloseHandle(hConnect);
        WinHttpCloseHandle(hSession);

        return true;
    }
}
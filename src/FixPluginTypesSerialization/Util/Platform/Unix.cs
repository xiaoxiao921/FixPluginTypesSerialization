using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.Util.Platform;

public static class Unix
{
    private const string LibCurl = "libcurl";

    [DllImport(LibCurl)]
    private static extern IntPtr curl_easy_init();

    [DllImport(LibCurl)]
    private static extern void curl_easy_cleanup(IntPtr handle);

    [DllImport(LibCurl)]
    private static extern int curl_easy_perform(IntPtr handle);

    [DllImport(LibCurl)]
    private static extern int curl_easy_setopt(IntPtr handle, int option, IntPtr value);

    [DllImport(LibCurl)]
    private static extern int curl_easy_setopt(IntPtr handle, int option, string value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate UIntPtr WriteCallback(IntPtr buffer, UIntPtr size, UIntPtr nmemb, IntPtr userdata);

    private const int CURLOPT_URL = 10002;
    private const int CURLOPT_WRITEFUNCTION = 20011;
    private const int CURLOPT_WRITEDATA = 10001;

    public static bool DownloadFile(string url, out byte[] data)
    {
        data = [];
        
        var curl = curl_easy_init();
        if (curl == IntPtr.Zero)
            return false;

        using var ms = new MemoryStream();

        var callbackPtr = Marshal.GetFunctionPointerForDelegate((WriteCallback)Callback);

        curl_easy_setopt(curl, CURLOPT_URL, url);
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, callbackPtr);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, IntPtr.Zero);

        var result = curl_easy_perform(curl);
        curl_easy_cleanup(curl);
        
        if (result != 0)
            return false;

        data = ms.ToArray();

        return true;

        UIntPtr Callback(IntPtr buffer, UIntPtr size, UIntPtr nmemb, IntPtr _)
        {
            var total = (long)size * (long)nmemb;
            var data = new byte[total];

            Marshal.Copy(buffer, data, 0, data.Length);

            // ReSharper disable once AccessToDisposedClosure
            // SAFETY: MemoryStream will not be disposed until curl action has completed (or has failed)
            //         so the MemoryStream will always be valid
            ms.Write(data, 0, data.Length);

            return checked((UIntPtr)(ulong)total);
        }
    }
}
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

internal class Program
{
    private const string unitySymbolServer = "https://symbolserver.unity3d.com/";

    private static async Task<int> Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Incorrect amount of arguments");
            return -1;
        }

        var url = args[0];
        if (!Uri.TryCreate(url, UriKind.Relative, out var uri))
        {
            Console.Error.WriteLine("Failed to parse url");
            return -1;
        }

        var filePath = args[1];
        try
        {
            Path.GetFullPath(filePath);
        }
        catch
        {
            Console.Error.WriteLine("File path is invalid");
            return -1;
        }

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(unitySymbolServer);
            Console.Out.WriteLine($"Downloading pdb from {new Uri(client.BaseAddress, uri)}");

            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine("Failed to download pdb file");
                return -1;
            }

            using (var file = File.Create(filePath))
            using (var contentStream = await response.Content.ReadAsStreamAsync())
            {
                Console.Out.WriteLine($"Writing the compressed pdb to {filePath}");
                await contentStream.CopyToAsync(file);
            }
        }

        return 0;
    }
}
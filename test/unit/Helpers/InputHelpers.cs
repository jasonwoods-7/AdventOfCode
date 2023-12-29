using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AoC.Tests.Properties;

namespace AoC.Tests.Helpers;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public static partial class InputHelpers
{
    [GeneratedRegex(@"Y(\d+)[/\\]Day(\d+)")]
    private static partial Regex PathRegex();

    public static async Task<IEnumerable<string>> ReadInputFileAsync(
        string fileName = "input.txt",
        [CallerFilePath] string callerFilePath = "")
    {
        var fullFileName = Path.Combine(
            Path.GetDirectoryName(callerFilePath)!,
            fileName);

        var exists = Path.Exists(fullFileName);

        if (!exists && fileName == "input.txt")
        {
            var session = ModuleInit.Configuration["session"];
            var match = PathRegex().Match(callerFilePath);

            if (session is not null && match.Success)
            {
                await DownloadInputAsync(session, match, fullFileName);

                exists = true;
            }
        }

        Skip.IfNot(exists, $"File \"{fullFileName}\" not found");

        return File.ReadLines(fullFileName);
    }

    static async Task DownloadInputAsync(string session, Match match, string fullFileName)
    {
        var aocUri = new Uri("https://adventofcode.com");

        var cookieContainer = new CookieContainer();
        cookieContainer.Add(aocUri, new Cookie("session", session));

        using var client = new HttpClient(new HttpClientHandler
        {
            CookieContainer = cookieContainer,
            AutomaticDecompression = DecompressionMethods.All
        });
        client.BaseAddress = aocUri;

        var response = await client
            .GetAsync($"{match.Groups[1].Value}/day/{match.Groups[2].Value.TrimStart('0')}/input");

        var text = await response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync();

        await File.WriteAllTextAsync(fullFileName, text);
    }
}

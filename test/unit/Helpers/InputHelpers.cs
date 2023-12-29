using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AoC.Tests.Properties;

namespace AoC.Tests.Helpers;

public static partial class InputHelpers
{
    [GeneratedRegex(@"Y(\d+)[/\\]Day(\d+)")]
    private static partial Regex PathRegex();

    public static IEnumerable<string> ReadInputFile(
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
                DownloadInput(session, match, fullFileName);

                exists = true;
            }
        }

        Skip.IfNot(exists, $"File \"{fullFileName}\" not found");

        return File.ReadLines(fullFileName);
    }

    static void DownloadInput(string session, Match match, string fullFileName)
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

        var response = client
            .GetAsync($"{match.Groups[1].Value}/day/{match.Groups[2].Value.TrimStart('0')}/input")
            .GetAwaiter()
            .GetResult();

        var text = response
            .EnsureSuccessStatusCode()
            .Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();

        File.WriteAllText(fullFileName, text);
    }
}

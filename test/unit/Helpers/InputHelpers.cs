using System.Runtime.CompilerServices;

namespace AoC.Tests.Helpers;

public static class InputHelpers
{
    public static IEnumerable<string> ReadInputFile(
        string fileName = "input.txt",
        [CallerFilePath] string callerFilePath = "")
    {
        var fullFileName = Path.Combine(
            Path.GetDirectoryName(callerFilePath)!,
            fileName);

        var exists = Path.Exists(fullFileName);

        Skip.IfNot(exists, $"File \"{fullFileName}\" not found");

        return File.ReadLines(fullFileName);
    }
}

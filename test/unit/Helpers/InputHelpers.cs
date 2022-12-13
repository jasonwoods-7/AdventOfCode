using System.Runtime.CompilerServices;

namespace AoC.Tests.Helpers;

public static class InputHelpers
{
    public static IEnumerable<string> ReadInputFile(
        string fileName = "input.txt",
        [CallerFilePath] string callerFilePath = "") =>
        File.ReadLines(
            Path.Combine(
                Path.GetDirectoryName(callerFilePath)!,
                fileName));
}

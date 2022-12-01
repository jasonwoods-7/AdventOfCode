using System.Runtime.CompilerServices;

namespace AoC.Tests.Helpers;

public static class InputHelpers
{
    public static string[] ReadInputFile(
        string fileName = "input.txt",
        [CallerFilePath] string callerFilePath = "") =>
        File.ReadAllLines(
            Path.Combine(
                Path.GetDirectoryName(callerFilePath)!,
                fileName));
}

using static MoreLinq.Extensions.BatchExtension;
using static MoreLinq.Extensions.FoldExtension;

namespace AoC.Y2022.Day03;

public class Day03 : IAoCRunner<string[], int>
{
    public string[] ParseInput(string[] puzzleInput) => puzzleInput;

    public int RunPart1(string[] input) => input
        .Select(static l =>
        {
            var midpoint = l.Length >> 1;
            return (l[..midpoint], l[midpoint..]);
        })
        .Sum(static n => n
            .Item1
            .Intersect(n.Item2)
            .Fold(GetPriority));

    public int RunPart2(string[] input) => input
        .Batch(3)
        .Sum(b => b
            .Fold(static (x, y, z) => x.Intersect(y).Intersect(z))
            .Fold(GetPriority));

    static int GetPriority(char item) => item switch
    {
        >= 'a' and <= 'z' => item - 'a' + 1,
        >= 'A' and <= 'Z' => item - 'A' + 27,
        _ => throw new InvalidOperationException($"Unexpected char in input: {item}")
    };
}

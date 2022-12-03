using static MoreLinq.Extensions.BatchExtension;

namespace AoC.Y2022.Day03;

public class Day03 : IAoCRunner<string[], int>
{
    public string[] ParseInput(string[] puzzleInput) => puzzleInput;

    public int RunPart1(string[] input) => input
        .Select(l =>
        {
            var midpoint = l.Length >> 1;
            return (l[..midpoint], l[midpoint..]);
        })
        .Sum(n => n
            .Item1
            .Intersect(n.Item2)
            .Apply(GetPriority));

    public int RunPart2(string[] input) => input
        .Select(l => (IEnumerable<char>)l.ToCharArray())
        .Batch(3)
        .Sum(b => b
            .Aggregate((r, c) => r.Intersect(c))
            .Apply(GetPriority));

    static int GetPriority(IEnumerable<char> items) => items
        .Single() switch
        {
            var c and >= 'a' and <= 'z' => c - 'a' + 1,
            var c and >= 'A' and <= 'Z' => c - 'A' + 27,
            var c => throw new InvalidOperationException($"Unexpected char in input: {c}")
        };
}

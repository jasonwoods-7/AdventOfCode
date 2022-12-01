using static MoreLinq.Extensions.SegmentExtension;

namespace AoC.Y2022.Day01;

public class Day01 : IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int>
{
    public IReadOnlyList<IReadOnlyList<int>> ParseInput(string[] puzzleInput) => puzzleInput
        .Select(l => int.TryParse(l, out var v) ? v : -1)
        .Segment(n => n == -1)
        .Select(ns => (IReadOnlyList<int>)ns.SkipWhile(n => n == -1).ToList())
        .ToList();

    public int RunPart1(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Select(s => s.Sum())
        .OrderDescending()
        .First();

    public int RunPart2(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Select(s => s.Sum())
        .OrderDescending()
        .Take(3)
        .Sum();
}

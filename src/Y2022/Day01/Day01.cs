using static MoreLinq.Extensions.SplitExtension;

namespace AoC.Y2022.Day01;

public class Day01 : IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int>
{
    public IReadOnlyList<IReadOnlyList<int>> ParseInput(string[] puzzleInput) => puzzleInput
        .Split(
            l => l == string.Empty,
            ls => (IReadOnlyList<int>)ls
                .Select(int.Parse)
                .ToList())
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

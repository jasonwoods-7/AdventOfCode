using static MoreLinq.Extensions.SplitExtension;

namespace AoC.Y2022.Day01;

public class Day01 : IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int>
{
    public IReadOnlyList<IReadOnlyList<int>> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Split(
            static l => l == string.Empty,
            static ls => (IReadOnlyList<int>)ls
                .Select(int.Parse)
                .ToList())
        .ToList();

    public int RunPart1(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Select(static s => s.Sum())
        .OrderDescending()
        .First();

    public int RunPart2(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Select(static s => s.Sum())
        .OrderDescending()
        .Take(3)
        .Sum();
}

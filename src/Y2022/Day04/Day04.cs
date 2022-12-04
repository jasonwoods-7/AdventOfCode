using System.Globalization;
using static MoreLinq.Extensions.FoldExtension;

namespace AoC.Y2022.Day04;

public partial class Day04 : IAoCRunner<IEnumerable<(int, int, int, int)>, int>
{
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex IntegerRegex();

    public IEnumerable<(int, int, int, int)> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(static l => IntegerRegex()
            .Matches(l)
            .Select(m => int.Parse(m.Value, CultureInfo.CurrentCulture))
            .Fold((a, b, x, y) => (a, b, x, y)));

    public int RunPart1(IEnumerable<(int, int, int, int)> input) => input
        .Select(GetRangesAndOverlap)
        .Count(static t =>
            t.overlap.Length == t.elf1Range.Length ||
            t.overlap.Length == t.elf2Range.Length);

    public int RunPart2(IEnumerable<(int, int, int, int)> input) => input
        .Select(GetRangesAndOverlap)
        .Count(static t => t.overlap.Length != 0);

    static (int[] elf1Range, int[] elf2Range, int[] overlap) GetRangesAndOverlap((int a, int b, int x, int y) input)
    {
        var elf1 = IntegerRange.FromMinMax(input.a, input.b, 1).ToArray();
        var elf2 = IntegerRange.FromMinMax(input.x, input.y, 1).ToArray();
        var overlap = elf1.Intersect(elf2).ToArray();

        return (elf1, elf2, overlap);
    }
}

namespace AoC.Y2022.Day04;

public class Day04 : IAoCRunner<(int, int, int, int)[], int>
{
    public (int, int, int, int)[] ParseInput(string[] puzzleInput) => puzzleInput
        .Select(static l =>
        {
            var match = Regex.Match(l, @"(\d+)-(\d+),(\d+)-(\d+)");
            Debug.Assert(match.Success);

            return (
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value),
                int.Parse(match.Groups[4].Value)
            );
        })
        .ToArray();

    public int RunPart1((int, int, int, int)[] input) => input
        .Select(GetRangesAndOverlap)
        .Count(static t =>
            t.overlap.Length == t.elf1Range.Length ||
            t.overlap.Length == t.elf2Range.Length);

    public int RunPart2((int, int, int, int)[] input) => input
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

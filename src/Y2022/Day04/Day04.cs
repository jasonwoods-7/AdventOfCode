namespace AoC.Y2022.Day04;

public class Day04 : IAoCRunner<(int[] elf1, int[] elf2, int[] overlap)[], int>
{
    public (int[], int[], int[])[] ParseInput(string[] puzzleInput) => puzzleInput
        .Select(static l =>
        {
            var match = Regex.Match(l, @"(\d+)-(\d+),(\d+)-(\d+)");
            Debug.Assert(match.Success);

            var elf1Begin = int.Parse(match.Groups[1].Value);
            var elf1End = int.Parse(match.Groups[2].Value);
            var elf1 = Enumerable.Range(elf1Begin, elf1End - elf1Begin + 1).ToArray();

            var elf2Begin = int.Parse(match.Groups[3].Value);
            var elf2End = int.Parse(match.Groups[4].Value);
            var elf2 = Enumerable.Range(elf2Begin, elf2End - elf2Begin + 1).ToArray();

            var overlap = elf1.Intersect(elf2).ToArray();

            return (elf1, elf2, overlap);
        })
        .ToArray();

    public int RunPart1((int[] elf1, int[] elf2, int[] overlap)[] input) => input
        .Count(static t =>
            t.overlap.Length == t.elf1.Length ||
            t.overlap.Length == t.elf2.Length);

    public int RunPart2((int[] elf1, int[] elf2, int[] overlap)[] input) => input
        .Count(static t => t.overlap.Length != 0);
}

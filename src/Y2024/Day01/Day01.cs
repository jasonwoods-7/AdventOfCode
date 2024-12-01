namespace AoC.Y2024.Day01;

public class Day01 : IAoCRunner<(IReadOnlyList<int>, IReadOnlyList<int>), int>
{
    public (IReadOnlyList<int>, IReadOnlyList<int>) ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .SelectMany(s => s.FindNumbers<int>())
            .Index()
            .Partition(t => (t.index & 1) == 0)
            .Apply(r => (r.True.Select(t => t.item).ToList(), r.False.Select(t => t.item).ToList()));

    public int RunPart1((IReadOnlyList<int>, IReadOnlyList<int>) input)
    {
        var left = input.Item1.Order().ToList();
        var right = input.Item2.Order().ToList();

        return left
            .Zip(right, (l, r) => Math.Abs(l - r))
            .Sum();
    }

    public int RunPart2((IReadOnlyList<int>, IReadOnlyList<int>) input)
    {
        var left = input.Item1;
        var right = input.Item2
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, g => g.Count());

        return left.Sum(i =>
        {
            if (right.TryGetValue(i, out var count))
            {
                return i * count;
            }

            return 0;
        });
    }
}

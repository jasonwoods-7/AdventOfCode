namespace AoC.Y2024.Day01;

public class Day01 : IAoCRunner<(IReadOnlyList<int>, IReadOnlyList<int>), int>
{
    public (IReadOnlyList<int>, IReadOnlyList<int>) ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .SelectMany(static s => s.FindNumbers<int>())
            .Index()
            .Partition(
                static t => (t.Index & 1) == 0,
                static (t, f) =>
                    (t.Select(static i => i.Item).ToList(), f.Select(static i => i.Item).ToList())
            );

    public int RunPart1(
        (IReadOnlyList<int>, IReadOnlyList<int>) input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var left = input.Item1.Order().ToList();
        var right = input.Item2.Order().ToList();

        return left.Zip(right, static (l, r) => Math.Abs(l - r)).Sum();
    }

    public int RunPart2(
        (IReadOnlyList<int>, IReadOnlyList<int>) input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var left = input.Item1;
        var right = input
            .Item2.GroupBy(static i => i)
            .ToDictionary(static g => g.Key, static g => g.Count());

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

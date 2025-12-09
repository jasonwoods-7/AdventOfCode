namespace AoC.Y2023.Day04;

public class Day04 : IAoCRunner<IReadOnlyList<int>, int>
{
    public IReadOnlyList<int> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(static line => line.Split('|'))
            .Select(static ss =>
                ss[0]
                    .FindNumbers<int>()
                    .Skip(1)
                    .ToImmutableHashSet()
                    .Intersect(ss[1].FindNumbers<int>())
                    .Count
            )
            .ToList();

    public int RunPart1(
        IReadOnlyList<int> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Sum(static t => (1 << t) >> 1);

    public int RunPart2(
        IReadOnlyList<int> input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var ticketCount = input.Select(_ => 1).ToList();

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 1; j <= input[i]; j++)
            {
                ticketCount[i + j] += ticketCount[i];
            }
        }

        return ticketCount.Sum();
    }
}

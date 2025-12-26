namespace AoC.Y2024.Day02;

public class Day02 : IAoCRunner<IReadOnlyList<Report>, int>
{
    public IReadOnlyList<Report> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(l => new Report(l.FindNumbers<int>().ToList())).ToList();

    public int RunPart1(
        IReadOnlyList<Report> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Count(r => IsReportSafe(r.Levels));

    public int RunPart2(
        IReadOnlyList<Report> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Count(r =>
            r.Levels.Subsets(r.Levels.Count - 1)
                .Cast<IReadOnlyList<int>>()
                .Append(r.Levels)
                .Any(IsReportSafe)
        );

    static bool IsReportSafe(IReadOnlyList<int> levels) =>
        levels
            .Zip(levels.Skip(1), (x, y) => (Math.Abs(x - y) is 1 or 2 or 3, x < y ? 1 : -1))
            .Aggregate(
                (res, t) =>
                    res switch
                    {
                        (true, -1) => (t is { Item1: true, Item2: -1 }, -1),
                        (true, 1) => (t is { Item1: true, Item2: 1 }, 1),
                        _ => res,
                    }
            )
            .Item1;
}

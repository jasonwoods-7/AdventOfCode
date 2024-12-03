namespace AoC.Y2024.Day02;

public record Report(IReadOnlyList<int> Levels);

public class Day02 : IAoCRunner<IReadOnlyList<Report>, int>
{
    public IReadOnlyList<Report> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => new Report(l.FindNumbers<int>().ToList()))
            .ToList();

    public int RunPart1(IReadOnlyList<Report> input) => input
        .Count(r => r
            .Levels
            .Zip(r.Levels.Skip(1), (x, y) => (Math.Abs(x - y) is 1 or 2 or 3, x < y))
            .Aggregate((true, 0), (res, t) =>
            {
                return res switch
                {
                    (true, -1) => (t is { Item1: true, Item2: false }, -1),
                    (true, 0) => (t.Item1, t.Item2 ? 1 : -1),
                    (true, 1) => (t is { Item1: true, Item2: true }, 1),
                    _ => res
                };
            }, res => res.Item1));

    public int RunPart2(IReadOnlyList<Report> input) => input
        .Count(r => r
            .Levels
            .Subsets(r.Levels.Count - 1)
            .Cast<IReadOnlyList<int>>()
            .Append(r.Levels)
            .Any(s => s
                .Zip(s.Skip(1), (x, y) => (Math.Abs(x - y) is 1 or 2 or 3, x < y))
                .Aggregate((true, 0), (res, t) =>
                {
                    return res switch
                    {
                        (true, -1) => (t is { Item1: true, Item2: false }, -1),
                        (true, 0) => (t.Item1, t.Item2 ? 1 : -1),
                        (true, 1) => (t is { Item1: true, Item2: true }, 1),
                        _ => res
                    };
                }, res => res.Item1)));
}

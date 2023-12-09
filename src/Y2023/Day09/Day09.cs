namespace AoC.Y2023.Day09;

public class Day09 : IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int>
{
    public IReadOnlyList<IReadOnlyList<int>> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(l => l.FindNumbers<int>().ToList())
        .ToList();

    public int RunPart1(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Sum(static e => Solve(ImmutableList.Create(e), FindNext));

    public int RunPart2(IReadOnlyList<IReadOnlyList<int>> input) => input
        .Sum(static e => Solve(ImmutableList.Create(e), FindPrevious));

    static int Solve(
        ImmutableList<IReadOnlyList<int>> current,
        Func<ImmutableList<IReadOnlyList<int>>, int> finder)
    {
        var previous = current[^1];

        if (previous.All(x => x == 0))
        {
            return finder(current);
        }

        var next = new List<int>();

        for (var i = 0; i < previous.Count - 1; i++)
        {
            next.Add(previous[i + 1] - previous[i]);
        }

        return Solve(current.Add(next), finder);
    }

    static int FindNext(ImmutableList<IReadOnlyList<int>> current) => current
        .Sum(c => c[^1]);

    static int FindPrevious(ImmutableList<IReadOnlyList<int>> current) => current
        .Select(c => c[0])
        .Reverse()
        .Aggregate((r, c) => c - r);
}

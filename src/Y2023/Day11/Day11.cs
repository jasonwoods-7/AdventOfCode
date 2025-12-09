namespace AoC.Y2023.Day11;

public class Day11(long multiplier) : IAoCRunner<Galaxy, long>
{
    public Galaxy ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .SelectMany(y => y.Item.Select((c, x) => (coord: new Coord(x, y.Index), item: c)))
            .Choose(t => (t.item == '#', t.coord))
            .Apply(g => new Galaxy(g.ToList()));

    public long RunPart1(
        Galaxy input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Solve(1L);

    public long RunPart2(
        Galaxy input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Solve(multiplier);
}

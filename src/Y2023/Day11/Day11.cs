namespace AoC.Y2023.Day11;

public record Galaxy(IReadOnlyList<Coord> Coords)
{
    public long Solve(long multiplier) => Expand(multiplier)
        .Coords
        .Subsets(2)
        .Sum(galaxies => galaxies.Fold((start, end) => start.ManhattanDistanceTo(end)));

    Galaxy Expand(long multiplier)
    {
        var maxX = Coords.MaxBy(c => c.X).X;

        var xEmpty = new List<long>();
        var yEmpty = new List<long>();

        for (var x = 0; x <= maxX; x++)
        {
            if (Coords.All(c => c.X != x))
            {
                xEmpty.Add(x);
            }

            if (Coords.All(c => c.Y != x))
            {
                yEmpty.Add(x);
            }
        }

        return new Galaxy(Coords
            .Select(c => new Coord(
                c.X + (xEmpty.Count(x => c.X > x) * multiplier),
                c.Y + (yEmpty.Count(y => c.Y > y) * multiplier)))
            .ToList());
    }
}

public class Day11(long multiplier) : IAoCRunner<Galaxy, long>
{
    public Galaxy ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Index()
        .SelectMany(y => y.item.Select((c, x) => (coord: new Coord(x, y.index), item: c)))
        .Choose(t => (t.item == '#', t.coord))
        .Apply(g => new Galaxy(g.ToList()));

    public long RunPart1(Galaxy input) => input.Solve(1L);

    public long RunPart2(Galaxy input) => input.Solve(multiplier);
}

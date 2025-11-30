namespace AoC.Y2023.Day11;

public record Galaxy(IReadOnlyList<Coord> Coords)
{
    public long Solve(long multiplier) =>
        Expand(multiplier)
            .Coords.Subsets(2)
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

        return new Galaxy(
            Coords
                .Select(c => new Coord(
                    c.X + (xEmpty.Count(x => c.X > x) * multiplier),
                    c.Y + (yEmpty.Count(y => c.Y > y) * multiplier)
                ))
                .ToList()
        );
    }
}

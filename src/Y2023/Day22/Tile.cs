namespace AoC.Y2023.Day22;

[DebuggerDisplay("{Start} -> {End}")]
public class Tile
{
    public Tile(Coord3d start, Coord3d end)
    {
        Start = start;
        End = end;

        Blocks = (
            from x in EnumerableExtensions.Range(start.X, end.X + 1, static x => x + 1)
            from y in EnumerableExtensions.Range(start.Y, end.Y + 1, static y => y + 1)
            from z in EnumerableExtensions.Range(start.Z, end.Z + 1, static z => z + 1)
            select new Coord3d(x, y, z)
        ).ToImmutableList();
    }

    public Coord3d Start { get; }
    public Coord3d End { get; }
    public ImmutableList<Coord3d> Blocks { get; }

    public long FallCount(ImmutableDictionary<Coord3d, Tile> allTiles)
    {
        if (Start.Z == 1)
        {
            return 0;
        }

        var fall = 1;

        while (
            Start.Z - fall >= 1
            && Blocks.All(c =>
            {
                if (allTiles.TryGetValue(c with { Z = c.Z - fall }, out var tile))
                {
                    return tile == this;
                }

                return true;
            })
        )
        {
            fall++;
        }

        return fall - 1;
    }

    public Tile Fall(long count) => new(Start - (0, 0, count), End - (0, 0, count));
}

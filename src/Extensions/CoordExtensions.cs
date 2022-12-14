namespace AoC.Extensions;

public static class CoordExtensions
{
    public static IEnumerable<Coord> Adjacent(this Coord source)
    {
        yield return new Coord(source.X - 1, source.Y - 1); // above-left
        yield return source with { Y = source.Y - 1 };      // above
        yield return new Coord(source.X + 1, source.Y - 1); // above-right

        yield return source with { X = source.X - 1 };      // left
        //yield return source;                              // current
        yield return source with { X = source.X + 1 };      // right

        yield return new Coord(source.X - 1, source.Y + 1); // below-left
        yield return source with { Y = source.Y + 1 };      // below
        yield return new Coord(source.X + 1, source.Y + 1); // below-right
    }

    public static IEnumerable<Coord> Neighbors(this Coord source)
    {
        yield return source with { Y = source.Y - 1 };  // above
        yield return source with { X = source.X - 1 };  // left
        yield return source with { X = source.X + 1 };  // right
        yield return source with { Y = source.Y + 1 };  // below
    }

    public static IEnumerable<Coord3d> Adjacent(this Coord3d source)
    {
        var dimensions = new[] { -1, 0, 1 };

        foreach (var z in dimensions)
        {
            foreach (var y in dimensions)
            {
                foreach (var x in dimensions)
                {
                    if ((x, y, z) == default)
                    {
                        continue;
                    }

                    yield return new Coord3d(source.X + x, source.Y + y, source.Z + z);
                }
            }
        }
    }

    public static IEnumerable<Coord3d> Neighbors(this Coord3d source)
    {
        yield return source with { Y = source.Y - 1 };  // above
        yield return source with { X = source.X - 1 };  // left
        yield return source with { X = source.X + 1 };  // right
        yield return source with { Y = source.Y + 1 };  // below
        yield return source with { Z = source.Z - 1 };  // plane-above
        yield return source with { Z = source.Z + 1 };  // plane-below
    }
}

namespace AoC.Extensions;

public static class CoordExtensions
{
    public static Coord AboveLeft(this Coord source) => new(source.X - 1, source.Y - 1);

    public static Coord Above(this Coord source) => source with { Y = source.Y - 1 };

    public static Coord AboveRight(this Coord source) => new(source.X + 1, source.Y - 1);

    public static Coord Left(this Coord source) => source with { X = source.X - 1 };

    public static Coord Right(this Coord source) => source with { X = source.X + 1 };

    public static Coord BelowLeft(this Coord source) => new(source.X - 1, source.Y + 1);

    public static Coord Below(this Coord source) => source with { Y = source.Y + 1 };

    public static Coord BelowRight(this Coord source) => new(source.X + 1, source.Y + 1);

    public static IEnumerable<Coord> Adjacent(this Coord source)
    {
        yield return source.AboveLeft();
        yield return source.Above();
        yield return source.AboveRight();
        yield return source.Left();
        yield return source.Right();
        yield return source.BelowLeft();
        yield return source.Below();
        yield return source.BelowRight();
    }

    public static IEnumerable<Coord> Neighbors(this Coord source)
    {
        yield return source.Above();
        yield return source.Left();
        yield return source.Right();
        yield return source.Below();
    }

    public static Coord TurnRight(this Coord source) =>
        source switch
        {
            (0, -1) => Coord.Right, // Up -> Right
            (1, 0) => Coord.Down, // Right -> Down
            (0, 1) => Coord.Left, // Down -> Left
            (-1, 0) => Coord.Up, // Left -> Up
            _ => throw new InvalidOperationException(),
        };

    public static Coord TurnLeft(this Coord source) =>
        source switch
        {
            (0, -1) => Coord.Left, // Up -> Left
            (-1, 0) => Coord.Down, // Left -> Down
            (0, 1) => Coord.Right, // Down -> Right
            (1, 0) => Coord.Up, // Right -> Up
            _ => throw new InvalidOperationException(),
        };

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
        yield return source with
        {
            Y = source.Y - 1,
        }; // above
        yield return source with
        {
            X = source.X - 1,
        }; // left
        yield return source with
        {
            X = source.X + 1,
        }; // right
        yield return source with
        {
            Y = source.Y + 1,
        }; // below
        yield return source with
        {
            Z = source.Z - 1,
        }; // plane-above
        yield return source with
        {
            Z = source.Z + 1,
        }; // plane-below
    }
}

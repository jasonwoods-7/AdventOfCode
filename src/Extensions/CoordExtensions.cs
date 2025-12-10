namespace AoC.Extensions;

public static class CoordExtensions
{
    extension(Coord source)
    {
        public Coord AboveLeft() => new(source.X - 1, source.Y - 1);

        public Coord Above() => source with { Y = source.Y - 1 };

        public Coord AboveRight() => new(source.X + 1, source.Y - 1);

        public Coord Left() => source with { X = source.X - 1 };

        public Coord Right() => source with { X = source.X + 1 };

        public Coord BelowLeft() => new(source.X - 1, source.Y + 1);

        public Coord Below() => source with { Y = source.Y + 1 };

        public Coord BelowRight() => new(source.X + 1, source.Y + 1);

        public IEnumerable<Coord> Adjacent()
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

        public IEnumerable<Coord> Neighbors()
        {
            yield return source.Above();
            yield return source.Left();
            yield return source.Right();
            yield return source.Below();
        }

        public Coord TurnRight() =>
            source switch
            {
                (0, -1) => Coord.Right, // Up -> Right
                (1, 0) => Coord.Down, // Right -> Down
                (0, 1) => Coord.Left, // Down -> Left
                (-1, 0) => Coord.Up, // Left -> Up
                _ => throw new InvalidOperationException(),
            };

        public Coord TurnLeft() =>
            source switch
            {
                (0, -1) => Coord.Left, // Up -> Left
                (-1, 0) => Coord.Down, // Left -> Down
                (0, 1) => Coord.Right, // Down -> Right
                (1, 0) => Coord.Up, // Right -> Up
                _ => throw new InvalidOperationException(),
            };

        public double DistanceTo(Coord other) =>
            Math.Sqrt(Math.Pow(source.X - other.X, 2) + Math.Pow(source.Y - other.Y, 2));
    }
}

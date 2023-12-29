namespace AoC.Y2023.Day17;

sealed record Crucible(Coord Position, Coord Direction, int StraightMoves)
{
    public static readonly Crucible Empty = new((0, 0), (0, 0), 0);

    public IEnumerable<Crucible> Neighbors(int minStraight, int maxStraight)
    {
        if (Direction == Empty.Direction)
        {
            yield return new Crucible((1, 0), (1, 0), 1);
            yield return new Crucible((0, 1), (0, 1), 1);
            yield break;
        }

        if (StraightMoves < maxStraight)
        {
            yield return this with
            {
                Position = Position + Direction,
                StraightMoves = StraightMoves + 1
            };
        }

        if (StraightMoves < minStraight)
        {
            yield break;
        }

        var turn = (Math.Abs(Direction.X), Math.Abs(Direction.Y)) switch
        {
            (0, 1) => new Coord(1, 0),
            (1, 0) => new Coord(0, 1),
            _ => throw new InvalidOperationException()
        };

        yield return new Crucible(Position + turn, turn, 1);
        yield return new Crucible(Position - turn, -turn, 1);
    }
}

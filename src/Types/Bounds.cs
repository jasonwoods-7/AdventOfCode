namespace AoC.Types;

public readonly record struct Bounds(Coord TopLeft, Coord BottomRight)
{
    public static Bounds FindBounds(IEnumerable<Coord> coords) =>
        coords.Aggregate(
            (minX: long.MaxValue, maxX: long.MinValue, minY: long.MaxValue, maxY: long.MinValue),
            (res, cur) =>
                (
                    Math.Min(res.minX, cur.X),
                    Math.Max(res.maxX, cur.X),
                    Math.Min(res.minY, cur.Y),
                    Math.Max(res.maxY, cur.Y)
                ),
            res => new Bounds((res.minX, res.minY), (res.maxX, res.maxY))
        );

    public bool WithinBounds(Coord coord) =>
        TopLeft.X <= coord.X
        && coord.X <= BottomRight.X
        && TopLeft.Y <= coord.Y
        && coord.Y <= BottomRight.Y;

    public bool AabbCollision(Bounds other)
    {
        var isToTheLeft = BottomRight.X <= other.TopLeft.X;
        var isToTheRight = TopLeft.X >= other.BottomRight.X;
        var isAbove = BottomRight.Y <= other.TopLeft.Y;
        var isBelow = TopLeft.Y >= other.BottomRight.Y;

        return isToTheLeft || isToTheRight || isAbove || isBelow;
    }

    public long Area => (BottomRight.Y - TopLeft.Y + 1) * (BottomRight.X - TopLeft.X + 1);
}

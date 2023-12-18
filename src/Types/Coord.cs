using System.Numerics;

namespace AoC.Types;

public readonly record struct Coord(long X, long Y)
    : IAdditionOperators<Coord, Coord, Coord>
    , IAdditiveIdentity<Coord, Coord>
    , IEqualityOperators<Coord, Coord, bool>
    , ISubtractionOperators<Coord, Coord, Coord>
    , IUnaryNegationOperators<Coord, Coord>
{
    public long ManhattanDistanceTo(Coord other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public void Deconstruct(out long x, out long y) =>
        (x, y) = (X, Y);

    public static readonly Coord Empty = (0, 0);
    public static readonly Coord Up = (0, -1);
    public static readonly Coord Left = (-1, 0);
    public static readonly Coord Down = (0, 1);
    public static readonly Coord Right = (1, 0);

    /// <inheritdoc />
    public static Coord operator +(Coord left, Coord right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <inheritdoc />
    public static Coord AdditiveIdentity => Empty;

    /// <inheritdoc />
    public static Coord operator -(Coord left, Coord right) =>
        new(left.X - right.X, left.Y - right.Y);

    /// <inheritdoc />
    public static Coord operator -(Coord value) =>
        new(-value.X, -value.Y);

    public static implicit operator Coord((long, long) tuple) => new(tuple.Item1, tuple.Item2);
}

public record struct Coord3d(int X, int Y, int Z);

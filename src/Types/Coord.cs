using System.Numerics;

namespace AoC.Types;

public readonly record struct Coord(long X, long Y)
    : IAdditionOperators<Coord, Coord, Coord>
    , IAdditiveIdentity<Coord, Coord>
    , IEqualityOperators<Coord, Coord, bool>
    , ISubtractionOperators<Coord, Coord, Coord>
{
    public long ManhattanDistanceTo(Coord other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public void Deconstruct(out long x, out long y) =>
        (x, y) = (X, Y);

    // ReSharper disable once UnassignedReadonlyField
    public static readonly Coord Empty;

    /// <inheritdoc />
    public static Coord operator +(Coord left, Coord right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <inheritdoc />
    public static Coord AdditiveIdentity => Empty;

    /// <inheritdoc />
    public static Coord operator -(Coord left, Coord right) =>
        new(left.X - right.X, left.Y - right.Y);

    public static Coord operator -(Coord source) =>
        new(-source.X, -source.Y);
}

public record struct Coord3d(int X, int Y, int Z);

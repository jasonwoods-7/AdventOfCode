using System.Numerics;

namespace AoC.Types;

public readonly record struct Coord(int X, int Y)
    : IAdditionOperators<Coord, Coord, Coord>
    , IAdditiveIdentity<Coord, Coord>
    , IEqualityOperators<Coord, Coord, bool>
{
    public int ManhattanDistanceTo(Coord other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    // ReSharper disable once UnassignedReadonlyField
    public static readonly Coord Empty;

    /// <inheritdoc />
    public static Coord operator +(Coord left, Coord right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <inheritdoc />
    public static Coord AdditiveIdentity => Empty;
}

public record struct Coord3d(int X, int Y, int Z);

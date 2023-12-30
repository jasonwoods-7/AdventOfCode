using System.Numerics;

namespace AoC.Types;

[DebuggerDisplay("({X}, {Y})")]
public readonly record struct Coord(long X, long Y)
    : IAdditiveIdentity<Coord, Coord>
    , IAdditionOperators<Coord, Coord, Coord>
    , IMultiplicativeIdentity<Coord, Coord>
    , IMultiplyOperators<Coord, Coord, Coord>
    , IMultiplyOperators<Coord, long, Coord>
    , IEqualityOperators<Coord, Coord, bool>
    , ISubtractionOperators<Coord, Coord, Coord>
    , IUnaryNegationOperators<Coord, Coord>
{
    public static readonly Coord Zero = (0, 0);
    public static readonly Coord One = (1, 1);
    public static readonly Coord UnitX = (1, 0);
    public static readonly Coord UnitY = (0, 1);

    public static readonly Coord Up = (0, -1);
    public static readonly Coord Left = (-1, 0);
    public static readonly Coord Down = (0, 1);
    public static readonly Coord Right = (1, 0);

    public long ManhattanDistanceTo(Coord other) =>
        Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    public void Deconstruct(out long x, out long y) =>
        (x, y) = (X, Y);

    public static implicit operator Coord((long, long) tuple) =>
        new(tuple.Item1, tuple.Item2);

    /// <inheritdoc />
    public static Coord AdditiveIdentity => Zero;

    /// <inheritdoc />
    public static Coord operator +(Coord left, Coord right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <inheritdoc />
    public static Coord MultiplicativeIdentity => One;

    /// <inheritdoc />
    public static Coord operator *(Coord left, Coord right) =>
        new(left.X * right.X, left.Y * right.Y);

    /// <inheritdoc />
    public static Coord operator *(Coord left, long right) =>
        new(left.X * right, left.Y * right);

    /// <inheritdoc />
    public static Coord operator -(Coord left, Coord right) =>
        new(left.X - right.X, left.Y - right.Y);

    /// <inheritdoc />
    public static Coord operator -(Coord value) =>
        new(-value.X, -value.Y);
}

[DebuggerDisplay("({X}, {Y}, {Z})")]
public readonly record struct Coord3d(long X, long Y, long Z)
    : IAdditiveIdentity<Coord3d, Coord3d>
    , IAdditionOperators<Coord3d, Coord3d, Coord3d>
    , IMultiplicativeIdentity<Coord3d, Coord3d>
    , IMultiplyOperators<Coord3d, Coord3d, Coord3d>
    , IMultiplyOperators<Coord3d, long, Coord3d>
    , IEqualityOperators<Coord3d, Coord3d, bool>
    , ISubtractionOperators<Coord3d, Coord3d, Coord3d>
    , IUnaryNegationOperators<Coord3d, Coord3d>
{
    public static readonly Coord3d Zero = (0, 0, 0);
    public static readonly Coord3d One = (1, 1, 1);
    public static readonly Coord3d UnitX = (1, 0, 0);
    public static readonly Coord3d UnitY = (0, 1, 0);
    public static readonly Coord3d UnitZ = (0, 0, 1);

    public void Deconstruct(out long x, out long y, out long z) =>
        (x, y, z) = (X, Y, Z);

    public static implicit operator Coord3d((long, long, long) tuple) =>
        new(tuple.Item1, tuple.Item2, tuple.Item3);

    /// <inheritdoc />
    public static Coord3d AdditiveIdentity => Zero;

    /// <inheritdoc />
    public static Coord3d operator +(Coord3d left, Coord3d right) =>
        new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    /// <inheritdoc />
    public static Coord3d MultiplicativeIdentity => One;

    /// <inheritdoc />
    public static Coord3d operator *(Coord3d left, Coord3d right) =>
        new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    /// <inheritdoc />
    public static Coord3d operator *(Coord3d left, long right) =>
        new(left.X * right, left.Y * right, left.Z * right);

    /// <inheritdoc />
    public static Coord3d operator -(Coord3d left, Coord3d right) =>
        new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    /// <inheritdoc />
    public static Coord3d operator -(Coord3d value) =>
        new(-value.X, -value.Y, -value.Z);
}

namespace AoC.Y2022.Day18;

sealed record Bounds(Coord3d Lower, Coord3d Upper)
{
    public bool WithinBounds(Coord3d location) =>
        Lower.X <= location.X && location.X <= Upper.X &&
        Lower.Y <= location.Y && location.Y <= Upper.Y &&
        Lower.Z <= location.Z && location.Z <= Upper.Z;
}

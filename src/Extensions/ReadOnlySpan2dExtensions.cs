using CommunityToolkit.HighPerformance;

namespace AoC.Extensions;

public static class ReadOnlySpan2dExtensions
{
    public static T At<T>(this ReadOnlySpan2D<T> source, Coord coord) =>
        source[(int)coord.Y, (int)coord.X];
}

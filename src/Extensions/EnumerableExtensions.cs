using System.Numerics;

namespace AoC.Extensions;

public static class EnumerableExtensions
{
    public static T Product<T>(this IEnumerable<T> source)
        where T : IMultiplicativeIdentity<T, T>, IMultiplyOperators<T, T, T> =>
        source.Aggregate(T.MultiplicativeIdentity, static (p, e) => p * e);

    public static IEnumerable<T> Range<T>(T start, T end, Func<T, T> next)
        where T : IEqualityOperators<T, T, bool>
    {
        for (var current = start; current != end; current = next(current))
        {
            yield return current;
        }
    }
}

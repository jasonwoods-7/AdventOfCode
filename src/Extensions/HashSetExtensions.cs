namespace AoC.Extensions;

public static class HashSetExtensions
{
    public static ImmutableHashSet<T> AddRange<T>(
        this ImmutableHashSet<T> source,
        IEnumerable<T> range
    ) => range.Aggregate(source, static (result, current) => result.Add(current));

    public static ISet<T> FluentRemove<T>(this ISet<T> source, T item)
    {
        source.Remove(item);
        return source;
    }
}

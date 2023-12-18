﻿namespace AoC.Extensions;

public static class HashSetExtensions
{
    public static ImmutableHashSet<T> AddRange<T>(
        this ImmutableHashSet<T> source,
        IEnumerable<T> range) =>
        range.Aggregate(source, static (result, current) => result.Add(current));
}

namespace AoC.Extensions;

public static class AnyOfExtensions
{
    public static AnyOf<T1Result, T2> MapFirst<T1, T1Result, T2>(
        this AnyOf<T1, T2> source,
        Func<T1, T1Result> mapFirst) =>
        source.BiMap(
            mapFirst,
            static s => s);

    public static AnyOf<T1, T2Result> MapSecond<T1, T2, T2Result>(
        this AnyOf<T1, T2> source,
        Func<T2, T2Result> mapSecond) =>
        source.BiMap(
            static f => f,
            mapSecond);

    public static AnyOf<T1Result, T2Result> BiMap<T1, T1Result, T2, T2Result>(
        this AnyOf<T1, T2> source,
        Func<T1, T1Result> mapFirst,
        Func<T2, T2Result> mapSecond) =>
        source.IsFirst
            ? mapFirst(source.First)
            : mapSecond(source.Second);

    public static TResult BiFold<T1, T2, TResult>(
        this AnyOf<T1, T2> source,
        Func<T1, TResult> foldFirst,
        Func<T2, TResult> foldSecond) =>
        source.IsFirst
            ? foldFirst(source.First)
            : foldSecond(source.Second);
}

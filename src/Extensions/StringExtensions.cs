namespace AoC.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex IntegerRegex();

    public static IEnumerable<T> FindNumbers<T>(this string source)
        where T : IParsable<T> =>
        IntegerRegex()
            .Matches(source)
            .Select(m => T.Parse(m.Value, CultureInfo.CurrentCulture));
}

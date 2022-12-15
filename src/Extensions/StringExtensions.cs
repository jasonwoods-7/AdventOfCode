using System.Numerics;

namespace AoC.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"((?:-?\d+)(?:\.\d+)?)")]
    private static partial Regex NumberRegex();

    public static IEnumerable<T> FindNumbers<T>(this string source)
        where T : IParsable<T>, INumber<T> =>
        NumberRegex()
            .Matches(source)
            .Select(m => T.Parse(m.Value, CultureInfo.CurrentCulture));
}

namespace AoC.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex IntegerRegex();

    public static IEnumerable<int> FindIntegers(this string source) => IntegerRegex()
        .Matches(source)
        .Select(m => int.Parse(m.Value, CultureInfo.CurrentCulture));
}

using ParsedInput = System.Collections.Immutable.ImmutableList<(long lo, long hi)>;

namespace AoC.Y2025.Day02;

public class Day02 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Single()
            .Split(',')
            .Select(r => r.FindNumbers<long>().Fold((l, h) => (l, -h)))
            .ToImmutableList();

    public long RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => Runner(input, Part1IsInvalid);

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => Runner(input, Part2IsInvalid);

    static long Runner(ParsedInput input, Func<long, bool> predicate) =>
        input
            .SelectMany(
                r => EnumerableExtensions.Range(r.lo, r.hi + 1, l => l + 1),
                (_, r) => predicate(r) ? r : 0
            )
            .Sum();

    static bool Part1IsInvalid(long id)
    {
        var s = id.ToString(CultureInfo.InvariantCulture);

        if ((s.Length & 1) == 1)
        {
            return false;
        }

        var half = s.Length >> 1;

        for (var counter = 0; counter < half; ++counter)
        {
            if (s[counter] != s[counter + half])
            {
                return false;
            }
        }

        return true;
    }

    static bool Part2IsInvalid(long id)
    {
        if (id < 10)
        {
            return false;
        }

        var s = id.ToString(CultureInfo.InvariantCulture);

        var half = (s.Length + 1) >> 1;

        for (var outer = 1; outer <= half; ++outer)
        {
            var pattern = s[..outer];

            var potentialMatch = s.Length % outer == 0;

            for (var inner = outer; potentialMatch && inner < s.Length; inner += outer)
            {
                var candidate = s[inner..(inner + outer)];

                potentialMatch = string.Equals(pattern, candidate, StringComparison.Ordinal);
            }

            if (potentialMatch)
            {
                return true;
            }
        }

        return false;
    }
}

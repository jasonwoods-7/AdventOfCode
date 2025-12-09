using ParsedInput = System.Collections.Immutable.ImmutableList<(char, int)>;

namespace AoC.Y2025.Day01;

public class Day01(ILogger<Day01> logger) : IAoCRunner<ParsedInput, int>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(p => (p[0], p[1..].ParseNumber<int>())).ToImmutableList();

    public int RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Aggregate(
            (current: 50, count: 0),
            (t, c) =>
            {
                var next =
                    (
                        t.current
                        + c switch
                        {
                            ('L', var n) => -n,
                            ('R', var n) => n,
                            _ => throw new InvalidOperationException(),
                        }
                    ) % 100;

                while (next < 0)
                {
                    next += 100;
                }

                return (next, t.count + (next == 0 ? 1 : 0));
            },
            t => t.count
        );

    public int RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Aggregate(
            (current: 50, count: 0),
            (t, c) =>
            {
                var zeroes = 0;

                var next =
                    t.current
                    + c switch
                    {
                        ('L', var n) => -n,
                        ('R', var n) => n,
                        _ => throw new InvalidOperationException(),
                    };

                var first = true;
                while (next < 0)
                {
                    if (t.current == 0 && first)
                    {
                        zeroes--;
                        first = false;
                    }

                    zeroes++;
                    next += 100;
                }

                if (next == 0)
                {
                    zeroes++;
                }

                while (next >= 100)
                {
                    zeroes++;
                    next -= 100;
                }

                logger.LogDebug("Total zeroes: {Zeroes}", t.count + zeroes);

                return (next, t.count + zeroes);
            },
            t => t.count
        );
}

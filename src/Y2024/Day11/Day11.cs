using System.Collections.Concurrent;

namespace AoC.Y2024.Day11;

public class Day11 : IAoCRunner<IReadOnlyList<string>, long>
{
    public IReadOnlyList<string> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Single().Split(' ');

    public long RunPart1(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => CountStones(input, 25, cancellationToken);

    public long RunPart2(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => CountStones(input, 75, cancellationToken);

    static long CountStones(
        IReadOnlyList<string> input,
        int blinkCount,
        CancellationToken cancellationToken
    )
    {
        var cache = new ConcurrentDictionary<(string, int), long>();
        return input.Sum(n =>
            CalculateBlinkCount(
                long.Parse(n, CultureInfo.InvariantCulture),
                blinkCount,
                cache,
                cancellationToken
            )
        );
    }

    static long CalculateBlinkCount(
        long number,
        int blinkCount,
        ConcurrentDictionary<(string, int), long> cache,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        return cache.GetOrAdd(
            (number.ToString(CultureInfo.InvariantCulture), blinkCount),
            static (key, state) =>
                key switch
                {
                    (_, 0) => 1,
                    ("0", _) => CalculateBlinkCount(
                        1,
                        key.Item2 - 1,
                        state.cache,
                        state.cancellationToken
                    ),
                    var (num, _) when (num.Length & 1) == 0 => CalculateBlinkCount(
                        long.Parse(num[..(num.Length / 2)], CultureInfo.InvariantCulture),
                        key.Item2 - 1,
                        state.cache,
                        state.cancellationToken
                    )
                        + CalculateBlinkCount(
                            long.Parse(num[(num.Length / 2)..], CultureInfo.InvariantCulture),
                            key.Item2 - 1,
                            state.cache,
                            state.cancellationToken
                        ),
                    var (num, _) => CalculateBlinkCount(
                        2024 * long.Parse(num, CultureInfo.InvariantCulture),
                        key.Item2 - 1,
                        state.cache,
                        state.cancellationToken
                    ),
                },
            (cache, cancellationToken)
        );
    }
}

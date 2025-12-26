using ParsedInput = System.Collections.Immutable.ImmutableDictionary<
    string,
    System.Collections.Immutable.ImmutableList<string>
>;

namespace AoC.Y2025.Day11;

public class Day11 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => l.Replace(":", "").Split(' '))
            .Append(["out"])
            .ToImmutableDictionary(s => s[0], s => s[1..].ToImmutableList());

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solver(input, "you", "out", [], cancellationToken);

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        (
            Solver(input, "svr", "fft", [], cancellationToken)
            * Solver(input, "fft", "dac", [], cancellationToken)
            * Solver(input, "dac", "out", [], cancellationToken)
        )
        + (
            Solver(input, "svr", "dac", [], cancellationToken)
            * Solver(input, "dac", "fft", [], cancellationToken)
            * Solver(input, "fft", "out", [], cancellationToken)
        );

    static long Solver(
        ParsedInput input,
        string current,
        string target,
        Dictionary<string, long> cache,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        return cache.TryGetValue(current, out var value)
            ? value
            : cache.FluentAdd(
                current,
                string.Equals(current, target, StringComparison.Ordinal)
                    ? 1
                    : input[current]
                        .Sum(next => Solver(input, next, target, cache, cancellationToken))
            );
    }
}

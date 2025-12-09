using ParsedInput = System.Collections.Immutable.ImmutableList<System.ReadOnlyMemory<long>>;

namespace AoC.Y2025.Day03;

public class Day03 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => new ReadOnlyMemory<long>(l.Select(c => (long)c - '0').ToArray()))
            .ToImmutableList();

    public long RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => input.Sum(v => SolveLine(v, 2));

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => input.Sum(v => SolveLine(v, 12));

    static long SolveLine(ReadOnlyMemory<long> line, int remaining, long accumulator = 0)
    {
        if (remaining == 0)
        {
            return accumulator;
        }

        var currentSlice = line[..^(remaining - 1)];

        var max = -1L;
        var maxIndex = -1;

        for (var counter = 0; counter < currentSlice.Length; ++counter)
        {
            var current = currentSlice.Span[counter];

            if (current > max)
            {
                max = current;
                maxIndex = counter;
            }
        }

        return SolveLine(line[(maxIndex + 1)..], remaining - 1, (accumulator * 10) + max);
    }
}

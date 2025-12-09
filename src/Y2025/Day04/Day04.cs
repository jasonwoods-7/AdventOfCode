using ParsedInput = System.Collections.Immutable.ImmutableHashSet<AoC.Types.Coord>;

namespace AoC.Y2025.Day04;

public class Day04 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .SelectMany(
                (line, y) => line.Cartesian([y]).Index(),
                (_, t) => (t.Item.Item1 == '@', x: t.Index, y: t.Item.Item2)
            )
            .Choose(t => (t.Item1, new Coord(t.x, t.y)))
            .ToImmutableHashSet();

    public long RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => input.Count(c => c.Adjacent().Count(input.Contains) < 4);

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var currentCount = input.Count;
        int previousCount;
        var removed = 0;

        do
        {
            previousCount = currentCount;

            var toRemove = input.Where(c => c.Adjacent().Count(input.Contains) < 4).ToList();

            input = input.Except(toRemove);

            currentCount = input.Count;
            removed += toRemove.Count;
        } while (previousCount != currentCount);

        return removed;
    }
}

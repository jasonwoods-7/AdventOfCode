using ParsedInput = System.Collections.Immutable.ImmutableList<AoC.Types.Coord>;

namespace AoC.Y2025.Day09;

public class Day09 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => l.FindNumbers<int>().Fold((x, y) => new Coord(x, y)))
            .ToImmutableList();

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Subsets(2)
            .Select(c => c.Fold((f, s) => Bounds.FindBounds([f, s]).Area))
            .OrderDescending()
            .First();

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var boundaries = input
            .Zip(input.Prepend(input.Last()))
            .Select(cs => Bounds.FindBounds([cs.Item1, cs.Item2]))
            .ToList();

        return input
            .Subsets(2)
            .Select(c => c.Fold((f, s) => Bounds.FindBounds([f, s])))
            .OrderByDescending(b => b.Area)
            .First(b => boundaries.All(b.AabbCollision))
            .Area;
    }
}

using AoC.Algorithms;
using ParsedInput = System.Collections.Immutable.ImmutableList<AoC.Types.Coord3d>;

namespace AoC.Y2025.Day08;

public class Day08 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => l.FindNumbers<int>().Fold((x, y, z) => new Coord3d(x, y, z)))
            .ToImmutableList();

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var (set, _) = Solver(input, (int)state!, cancellationToken);

        return input.GroupBy(set.Find).Select(g => g.Count()).OrderDescending().Take(3).Product();
    }

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var (_, ((x1, _, _), (x2, _, _))) = Solver(input, -1, cancellationToken);

        return x1 * x2;
    }

    static (DisjointUnionSet<Coord3d> set, (Coord3d, Coord3d) lastAdded) Solver(
        ParsedInput input,
        int remainingConnections,
        CancellationToken cancellationToken
    ) =>
        input
            .Subsets(2)
            .OrderBy(c => c.Fold((f, s) => f.DistanceTo(s)))
            .Aggregate(
                (
                    set: new DisjointUnionSet<Coord3d>(input),
                    lastAdded: (Coord3d.Zero, Coord3d.Zero),
                    remainingConnections
                ),
                (accumulator, current) =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (accumulator.remainingConnections == 0)
                    {
                        return accumulator;
                    }

                    var lastAdded = accumulator.set.Union(current[0], current[1])
                        ? (current[0], current[1])
                        : accumulator.lastAdded;

                    return (accumulator.set, lastAdded, accumulator.remainingConnections - 1);
                },
                accumulator => (accumulator.set, accumulator.lastAdded)
            );
}

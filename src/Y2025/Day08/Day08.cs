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
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var (boxes, _) = Solver(input, (int)additionalParams![0], cancellationToken);

        return boxes.Select(b => b.Count).OrderDescending().Take(3).Product();
    }

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var (_, lastAdded) = Solver(input, -1, cancellationToken);

        return lastAdded.Item1.X * lastAdded.Item2.X;
    }

    static (ImmutableList<ImmutableHashSet<Coord3d>> boxes, (Coord3d, Coord3d) lastAdded) Solver(
        ParsedInput input,
        int remainingConnections,
        CancellationToken cancellationToken
    ) =>
        input
            .Subsets(2)
            .OrderBy(c => c.Fold((f, s) => f.DistanceTo(s)))
            .Aggregate(
                (
                    boxes: ImmutableList<ImmutableHashSet<Coord3d>>.Empty,
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

                    var indexes = accumulator
                        .boxes.Index()
                        .Choose(t =>
                            (t.Item.Contains(current[0]) || t.Item.Contains(current[1]), t.Index)
                        )
                        .ToList();

                    if (indexes.Count == 0)
                    {
                        return (
                            accumulator.boxes.Add(
                                ImmutableHashSet<Coord3d>.Empty.AddRange([current[0], current[1]])
                            ),
                            (current[0], current[1]),
                            accumulator.remainingConnections - 1
                        );
                    }

                    var next = accumulator.boxes;
                    var junction = accumulator.boxes[indexes[0]].AddRange([current[0], current[1]]);

                    var lastAdded =
                        junction == accumulator.boxes[indexes[0]]
                            ? accumulator.lastAdded
                            : (current[0], current[1]);

                    for (var counter = 1; counter < indexes.Count; ++counter)
                    {
                        junction = junction.AddRange(accumulator.boxes[indexes[counter]]);
                        next = accumulator.boxes.RemoveAt(indexes[counter] - counter + 1);
                    }

                    return (
                        next.SetItem(indexes[0], junction),
                        lastAdded,
                        accumulator.remainingConnections - 1
                    );
                },
                accumulator => (accumulator.boxes, accumulator.lastAdded)
            );
}

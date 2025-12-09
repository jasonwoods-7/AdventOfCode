namespace AoC.Y2022.Day09;

public class Day09 : IAoCRunner<IEnumerable<(Direction, int)>, int>
{
    public IEnumerable<(Direction, int)> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(l =>
            l.Split(" ")
                .Fold(
                    (d, n) =>
                        (
                            d switch
                            {
                                "U" => Direction.Up,
                                "D" => Direction.Down,
                                "L" => Direction.Left,
                                "R" => Direction.Right,
                                _ => throw new InvalidOperationException(),
                            },
                            int.Parse(n, CultureInfo.CurrentCulture)
                        )
                )
        );

    public int RunPart1(
        IEnumerable<(Direction, int)> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => FindTailPositionCountForKnots(input, 2);

    public int RunPart2(
        IEnumerable<(Direction, int)> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => FindTailPositionCountForKnots(input, 10);

    static int FindTailPositionCountForKnots(IEnumerable<(Direction, int)> moves, int knotCount)
    {
        return moves.Aggregate(
            (
                knots: ImmutableArray.CreateRange(Enumerable.Repeat((x: 0, y: 0), knotCount)),
                tailPositions: ImmutableHashSet.Create((0, 0))
            ),
            static (state, move) =>
            {
                var intComparer = Comparer<int>.Default;

                var (knots, tailPositions) = state;
                var (direction, count) = move;

                var (xDelta, yDelta) = direction switch
                {
                    Direction.Up => (0, -1),
                    Direction.Down => (0, 1),
                    Direction.Left => (-1, 0),
                    Direction.Right => (1, 0),
                    _ => throw new InvalidOperationException(),
                };

                for (var counter = 0; counter < count; ++counter)
                {
                    var headPosition = knots[0];
                    headPosition = (headPosition.x + xDelta, headPosition.y + yDelta);
                    knots = knots.SetItem(0, headPosition);

                    for (var tailCounter = 1; tailCounter < knots.Length; ++tailCounter)
                    {
                        var tailPosition = knots[tailCounter];

                        if (AdjacentVertices(tailPosition).Contains(headPosition))
                        {
                            break;
                        }

                        tailPosition = (
                            tailPosition.x - intComparer.Compare(tailPosition.x, headPosition.x),
                            tailPosition.y - intComparer.Compare(tailPosition.y, headPosition.y)
                        );
                        knots = knots.SetItem(tailCounter, tailPosition);

                        Debug.Assert(
                            AdjacentVertices(tailPosition).Contains(headPosition),
                            "AdjacentVertices(tailPosition).Contains(headPosition)"
                        );

                        headPosition = tailPosition;
                    }

                    tailPositions = tailPositions.Add(knots[^1]);
                }

                return (knots, tailPositions);
            },
            static final => final.tailPositions.Count
        );

        static (int, int)[] AdjacentVertices((int x, int y) point) =>
            new[]
            {
                (point.x - 1, point.y - 1),
                (point.x - 1, point.y - 0),
                (point.x - 1, point.y + 1),
                (point.x - 0, point.y - 1),
                (point.x - 0, point.y - 0),
                (point.x - 0, point.y + 1),
                (point.x + 1, point.y - 1),
                (point.x + 1, point.y - 0),
                (point.x + 1, point.y + 1),
            };
    }
}

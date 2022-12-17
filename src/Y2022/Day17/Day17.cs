namespace AoC.Y2022.Day17;

public partial class Day17 : IAoCRunner<IEnumerable<Func<Coord, Coord>>, long>
{
    readonly ILogger<Day17> _logger;

    public Day17(
        ILogger<Day17> logger) =>
        _logger = logger;

    public IEnumerable<Func<Coord, Coord>> ParseInput(IEnumerable<string> puzzleInput)
    {
        var moves = puzzleInput.First();

        // ReSharper disable ConvertToLocalFunction
        var moveLeft = (Coord c) => c.Left();
        var moveRight = (Coord c) => c.Right();
        // ReSharper restore ConvertToLocalFunction

        while (true)
        {
            foreach (var move in moves)
            {
                Debug.Assert(move is '<' or '>');

                yield return move == '<'
                    ? moveLeft
                    : moveRight;
            }
        }

        // ReSharper disable once IteratorNeverReturns
    }

    public long RunPart1(IEnumerable<Func<Coord, Coord>> input) => SimulateFallingRocks(input, 2022);

    public long RunPart2(IEnumerable<Func<Coord, Coord>> input) => SimulateFallingRocks(input, 1_000_000_000_000);

    long SimulateFallingRocks(IEnumerable<Func<Coord, Coord>> gusts, long totalRocks)
    {
        const int boardSize = 7;

        var grid = Enumerable
            .Range(0, boardSize)
            .Select(x => new Coord(x, 0))
            .ToHashSet();

        var totalHeight = 0L;

        var foundRepeat = false;

        using var gustEnumerator = gusts.GetEnumerator();
        using var shapeEnumerator = Shapes().GetEnumerator();

        for (var r = 0L; r < totalRocks; ++r)
        {
            shapeEnumerator.MoveNext();
            var currentShape = shapeEnumerator.Current;
            var minY = grid.MinBy(c => c.Y).Y;

            if (!foundRepeat && CheckForRepetition(grid, minY, r + 1) is { IsSome: true } result)
            {
                var (height, rock) = result.IfNone((0, 0));
                var repeatHeight = -(minY - height);
                var repeatRocks = (r + 1) - rock;

                var (multiplier, _) = Math.DivRem(totalRocks - r, repeatRocks);
                r += repeatRocks * multiplier;
                totalHeight += repeatHeight * multiplier;

                foundRepeat = true;
            }

            var fallingShape = currentShape
                .Select(c => c with { Y = minY + c.Y })
                .ToList();

            while (true)
            {
                // gust of wind
                gustEnumerator.MoveNext();
                var mover = gustEnumerator.Current;
                var shiftedShape = fallingShape
                    .Select(c => mover(c))
                    .ToList();

                // check if out of bounds
                if (!IsInvalidNextPosition(grid, shiftedShape) && shiftedShape.All(c => c.X is >= 0 and < boardSize))
                {
                    fallingShape = shiftedShape;
                }

                // fall
                var nextPosition = fallingShape
                    .Select(c => c.Below())
                    .ToList();

                // check if at resting position
                if (IsInvalidNextPosition(grid, nextPosition))
                {
                    foreach (var current in fallingShape)
                    {
                        grid.Add(current);
                    }

                    var shapeY = fallingShape.MinBy(c => c.Y).Y;
                    var heightAdded = -(shapeY - minY);

                    if (heightAdded > 0)
                    {
                        totalHeight += heightAdded;
                    }

                    break;
                }

                fallingShape = nextPosition;
            }
        }

        // _logger.LogDebug("{Grid}", RenderGrid(grid));

        return totalHeight;
    }

    static bool IsInvalidNextPosition(
        IReadOnlySet<Coord> grid,
        IReadOnlyList<Coord> currentShape) =>
        currentShape.Any(grid.Contains);
}

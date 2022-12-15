namespace AoC.Y2022.Day15;

public class Day15 : IAoCRunner<IReadOnlyList<(Coord, Coord)>, long>
{
    readonly int _yPosOrMax;
    readonly ILogger<Day15> _logger;

    public Day15(
        int yPosOrMax,
        ILogger<Day15> logger)
    {
        _yPosOrMax = yPosOrMax;
        _logger = logger;
    }

    public IReadOnlyList<(Coord, Coord)> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(static l => l.FindNumbers<int>())
        .Select(static ns => ns.Fold(static (sx, sy, bx, by) => (new Coord(sx, sy), new Coord(bx, by))))
        .ToList();

    public long RunPart1(IReadOnlyList<(Coord, Coord)> input)
    {
        var seenX = new System.Collections.Generic.HashSet<int>();

        foreach (var (signal, beacon) in input)
        {
            var distance = signal.ManhattanDistanceTo(beacon);

            for (var x = 0; x <= distance; ++x)
            {
                var y = distance - x;

                if (signal.Y + y == _yPosOrMax)
                {
                    seenX.Add(signal.X + x);
                    seenX.Add(signal.X - x);
                }
                else if (signal.Y - y == _yPosOrMax)
                {
                    seenX.Add(signal.X + x);
                    seenX.Add(signal.X - x);
                }

                Debug.Assert(new Coord(signal.X + x, signal.Y + y).ManhattanDistanceTo(signal) == distance);
            }
        }

        return seenX.Max()
            - seenX.Min()
            - input.Select(b => b.Item2).Distinct().Count(b => b.Y == _yPosOrMax)
            + 1;
    }

    public long RunPart2(IReadOnlyList<(Coord, Coord)> input)
    {
        var distressBeacon = FindDistressBeacon(input);

        return (distressBeacon.X * 4_000_000L) + distressBeacon.Y;
    }

    static bool IsSeen(IReadOnlyList<(Coord, int)> signals, Coord testCoord) => signals
        .Any(t =>
        {
            var (signal, distance) = t;

            return testCoord.ManhattanDistanceTo(signal) <= distance;
        });

    Coord FindDistressBeacon(IReadOnlyList<(Coord, Coord)> input)
    {
        var distances = input
            .GroupBy(t => t.Item1, t => t.Item1.ManhattanDistanceTo(t.Item2))
            .Select(g => (g.Key, g.Max()))
            .OrderByDescending(t => t.Item2)
            .ToList();

        foreach (var (signal, beacon) in input)
        {
            var distanceToEdge = signal.ManhattanDistanceTo(beacon);

            for (var xPos = 0; xPos <= distanceToEdge; ++xPos)
            {
                var yPos = distanceToEdge - xPos;

                foreach (var coord in new[]
                         {
                             new Coord(signal.X + xPos, signal.Y + yPos),
                             new Coord(signal.X + xPos, signal.Y - yPos),
                             new Coord(signal.X - xPos, signal.Y + yPos),
                             new Coord(signal.X - xPos, signal.Y - yPos)
                         })
                {
                    foreach (var neighbor in coord.Neighbors())
                    {
                        if (0 <= neighbor.X && neighbor.X <= _yPosOrMax &&
                            0 <= neighbor.Y && neighbor.Y <= _yPosOrMax &&
                            !IsSeen(distances, neighbor) &&
                            neighbor.Neighbors().All(n => IsSeen(distances, n)))
                        {
                            return neighbor;
                        }
                    }
                }
            }
        }

        throw new InvalidOperationException();
    }
}

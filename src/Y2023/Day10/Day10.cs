using System.Text;

namespace AoC.Y2023.Day10;

public class Day10(ILogger<Day10> logger) : IAoCRunner<IReadOnlyDictionary<Coord, char>, int>
{
    public IReadOnlyDictionary<Coord, char> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .SelectMany(y => y.Item.Select((c, x) => (coord: new Coord(x, y.Index), item: c)))
            .Where(t => t.item != '.')
            .ToDictionary(t => t.coord, t => t.item);

    public int RunPart1(
        IReadOnlyDictionary<Coord, char> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => FindPipes(input).Max(v => v.Value);

    public int RunPart2(
        IReadOnlyDictionary<Coord, char> input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var pipes = FindPipes(input);

        var (maxX, maxY) = input.Keys.Aggregate(
            (x: -1L, y: -1L),
            (max, current) =>
                (current.X > max.x, current.Y > max.y) switch
                {
                    (true, true) => (current.X, current.Y),
                    (true, false) => (current.X, max.x),
                    (false, true) => (max.x, current.Y),
                    (false, false) => max,
                }
        );

        var map = new Dictionary<Coord, char>();

        for (var x = -1; x <= maxX + 1; x++)
        {
            for (var y = -1; y <= maxY + 1; y++)
            {
                map.Add(new Coord(x, y), 'I');
            }
        }

        foreach (var pipe in pipes)
        {
            map[pipe.Key] = input[pipe.Key];
        }

        var (start, _, _, startPipe) = FindStart(map);
        map[start] = startPipe;

        for (var y = maxY + 1; y >= -1; y--)
        {
            var inside = false;
            for (var x = maxX + 1; x >= -1; x--)
            {
                var key = new Coord(x, y);
                var value = map[key];
                if (!inside && value == 'I')
                {
                    map[key] = 'O';
                }

                if (value is '|' or 'L' or 'J')
                {
                    inside = !inside;
                }
            }
        }

        PrintMap(map, maxX, maxY);

        return map.Values.Count(v => v == 'I');
    }

    static IReadOnlyDictionary<Coord, int> FindPipes(IReadOnlyDictionary<Coord, char> input)
    {
        var (start, startNext, startPrev, _) = FindStart(input);

        var costs = input.ToDictionary(k => k.Key, _ => int.MaxValue);
        costs[start] = 0;

        var queue = new Queue<Coord>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            var cost = costs[current] + 1;

            var (next, prev) = input[current] switch
            {
                '|' => (current.Above(), current.Below()),
                '-' => (current.Left(), current.Right()),
                'L' => (current.Above(), current.Right()),
                'J' => (current.Above(), current.Left()),
                '7' => (current.Left(), current.Below()),
                'F' => (current.Right(), current.Below()),
                'S' => (startNext, startPrev),
                _ => throw new InvalidOperationException(),
            };

            if (costs.TryGetValue(next, out var nextCost) && cost < nextCost)
            {
                costs[next] = cost;
                queue.Enqueue(next);
            }

            if (costs.TryGetValue(prev, out var prevCost) && cost < prevCost)
            {
                costs[prev] = cost;
                queue.Enqueue(prev);
            }
        }

        return costs.Where(c => c.Value != int.MaxValue).ToDictionary(k => k.Key, v => v.Value);
    }

    static (Coord start, Coord next, Coord prev, char value) FindStart(
        IReadOnlyDictionary<Coord, char> map
    )
    {
        var start = map.Choose(t => (t.Value == 'S', t.Key)).First();

        var (next, prev, value) = (
            map.GetValueOrDefault(start.Above(), '.') is '|' or '7' or 'F',
            map.GetValueOrDefault(start.Left(), '.') is '-' or 'L' or 'F',
            map.GetValueOrDefault(start.Right(), '.') is '-' or 'J' or '7',
            map.GetValueOrDefault(start.Below(), '.') is '|' or 'L' or 'J'
        ) switch
        {
            (true, true, false, false) => (start.Above(), start.Left(), 'J'), // above & left match
            (true, false, true, false) => (start.Above(), start.Right(), 'L'), // above & right match
            (true, false, false, true) => (start.Above(), start.Below(), '|'), // above & below match
            (false, true, true, false) => (start.Left(), start.Right(), '-'), // left & right match
            (false, true, false, true) => (start.Left(), start.Below(), '7'), // left & below match
            (false, false, true, true) => (start.Right(), start.Below(), 'F'), // right & below match
            _ => throw new InvalidOperationException(),
        };

        return (start, next, prev, value);
    }

    void PrintMap(IReadOnlyDictionary<Coord, char> map, long maxX, long maxY)
    {
        for (var y = -1; y <= maxY + 1; y++)
        {
            var lineBuilder = new StringBuilder();
            for (var x = -1; x <= maxX + 1; x++)
            {
                var current = map[new Coord(x, y)];
                lineBuilder.Append(current == 'O' ? '.' : current);
            }

#pragma warning disable CA1848
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("{Line}", lineBuilder.ToString());
            }
#pragma warning restore CA1848
        }
    }
}

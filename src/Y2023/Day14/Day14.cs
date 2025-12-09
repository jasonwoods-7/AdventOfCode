using System.Text;
using Dish = System.Collections.Generic.IReadOnlyDictionary<AoC.Types.Coord, char>;

namespace AoC.Y2023.Day14;

public class Day14(ILogger<Day14> logger) : IAoCRunner<Dish, long>
{
    public Dish ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .SelectMany(y => y.Item.Select((c, x) => (new Coord(x, y.Index), c)))
            .ToDictionary();

    public long RunPart1(
        Dish input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var tilt = Tilt(input, 0, 1, 0, 1, (h, v) => new Coord(h, v));

        var maxY = input.Select(c => c.Key.Y).Max();

        return Score(tilt, maxY);
    }

    public long RunPart2(
        Dish input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var maxY = input.Select(c => c.Key.Y).Max();

        var spun = MultiSpin(input, 1_000, maxY);

        PrintMap(spun, maxY);

        return Score(spun, maxY);
    }

    void PrintMap(Dish dish, long max)
    {
        for (var y = 0; y <= max; ++y)
        {
            var builder = new StringBuilder((int)max);

            for (var x = 0; x <= max; ++x)
            {
                builder.Append(dish[new Coord(x, y)]);
            }

#pragma warning disable CA1848
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("{Line}", builder.ToString());
            }
#pragma warning restore CA1848
        }
    }

    static long Score(Dish dish, long max) =>
        dish.Where(v => v.Value == 'O').Sum(k => max - k.Key.Y + 1);

    static Dish MultiSpin(Dish dish, long count, long max)
    {
        while (count-- > 0)
        {
            dish = Spin(dish, max);
        }

        return dish;
    }

    static Dish Spin(Dish dish, long max) =>
        dish.Apply(d => Tilt(d, 0, 1, 0, 1, (h, v) => new Coord(h, v)))
            .Apply(d => Tilt(d, max, -1, 0, 1, (h, v) => new Coord(v, h)))
            .Apply(d => Tilt(d, max, -1, max, -1, (h, v) => new Coord(h, v)))
            .Apply(d => Tilt(d, 0, 1, max, -1, (h, v) => new Coord(v, h)));

    static Dish Tilt(
        Dish dish,
        long hStart,
        long hInc,
        long vStart,
        long vInc,
        Func<long, long, Coord> createCoord
    )
    {
        var nextDish = dish.ToDictionary(k => k.Key, v => v.Value);

        for (
            var horizontal = hStart;
            dish.ContainsKey(createCoord(horizontal, 0));
            horizontal += hInc
        )
        {
            var lastV = vStart;

            for (
                var vertical = vStart;
                dish.ContainsKey(createCoord(0, vertical));
                vertical += vInc
            )
            {
                var current = dish[createCoord(horizontal, vertical)];

                if (current == '#')
                {
                    lastV = vertical + vInc;
                }
                else if (current == 'O')
                {
                    nextDish[createCoord(horizontal, vertical)] = '.';
                    nextDish[createCoord(horizontal, lastV)] = 'O';
                    lastV += vInc;
                }
            }
        }

        return nextDish;
    }
}

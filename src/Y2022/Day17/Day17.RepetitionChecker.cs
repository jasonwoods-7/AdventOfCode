using System.Text;

namespace AoC.Y2022.Day17;

partial class Day17
{
    readonly Dictionary<string, (long, long)> _cache = new(StringComparer.Ordinal);

    Option<(long height, long rocks)> CheckForRepetition(
        IReadOnlySet<Coord> grid,
        long minY,
        long rock
    )
    {
        var normalizedRows = new StringBuilder(1000);

        for (var y = 0; y < 100; ++y)
        {
            for (var x = 0; x < 7; ++x)
            {
                normalizedRows.Append(grid.Contains(new Coord(x, minY + y)) ? '#' : ' ');
            }
        }

        var key = normalizedRows.ToString();
        if (_cache.TryGetValue(key, out var value))
        {
            return value;
        }

        _cache.Add(key, (minY, rock));

        return None;
    }
}

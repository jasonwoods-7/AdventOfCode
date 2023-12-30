namespace AoC.Y2023.Day22;

public class Day22 : IAoCRunner<ImmutableHashSet<Tile>, long>
{
    public ImmutableHashSet<Tile> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(l => l.FindNumbers<long>()
            .Fold((x1, y1, z1, x2, y2, z2) => new Tile((x1, y1, z1), (x2, y2, z2))))
        .ToImmutableHashSet();

    public long RunPart1(ImmutableHashSet<Tile> input)
    {
        var (fallenTower, _) = FallTiles(input);

        return fallenTower
            .AsParallel()
            .Count(t =>
            {
                var testTower = fallenTower.Remove(t);
                var (_, count) = FallTiles(testTower);
                return count == 0;
            });
    }

    public long RunPart2(ImmutableHashSet<Tile> input)
    {
        var (fallenTower, _) = FallTiles(input);

        return fallenTower
            .AsParallel()
            .Sum(t =>
            {
                var testTower = fallenTower.Remove(t);
                var (_, count) = FallTiles(testTower);
                return count;
            });
    }

    static (ImmutableHashSet<Tile>, long) FallTiles(ImmutableHashSet<Tile> tiles)
    {
        var previousTiles = tiles
            .SelectMany(t => t.Blocks, (t, b) => (b, t))
            .ToImmutableDictionary(t => t.b, t => t.t);

        var maxZ = tiles.Select(t => t.Start.Z).Max();

        var totalFallen = 0L;

        for (var z = 1L; z <= maxZ; z++)
        {
            var fallCandidates = previousTiles
                .Where(t => t.Key.Z == z)
                .GroupBy(t => t.Value)
                .Select(g => g.Key)
                .ToHashSet();

            var nextTiles = previousTiles;

            foreach (var candidate in fallCandidates)
            {
                var fallCount = candidate.FallCount(nextTiles);
                if (fallCount != 0)
                {
                    totalFallen++;
                    nextTiles = nextTiles
                        .RemoveRange(candidate.Blocks)
                        .AddRange(candidate.Fall(fallCount).Apply(t => t.Blocks.Select(b => new KeyValuePair<Coord3d, Tile>(b, t))));
                }
            }

            previousTiles = nextTiles;
        }

        var fallenTower = previousTiles
            .Values
            .ToImmutableHashSet();

        return (fallenTower, totalFallen);
    }
}

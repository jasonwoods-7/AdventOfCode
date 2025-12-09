namespace AoC.Y2023.Day17;

public class Day17 : IAoCRunner<int[][], int>
{
    public int[][] ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(l => l.Select(c => c - '0').ToArray()).ToArray();

    public int RunPart1(
        int[][] input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => MinHeatLoss(input, 0, 3);

    public int RunPart2(
        int[][] input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => MinHeatLoss(input, 4, 10);

    static int MinHeatLoss(int[][] map, int minStraight, int maxStraight)
    {
        var maxY = map.Length;
        var maxX = map[0].Length;

        var destination = new Coord(maxX - 1, maxY - 1);

        return SuperEnumerable.GetShortestPathCost<Crucible, int>(
            Crucible.Empty,
            (crucible, cost) =>
                crucible
                    .Neighbors(minStraight, maxStraight)
                    .Where(n =>
                        0 <= n.Position.X
                        && n.Position.X < maxX
                        && 0 <= n.Position.Y
                        && n.Position.Y < maxY
                    )
                    .Select(c => (c, cost + map[c.Position.Y][c.Position.X])),
            c => c.Position == destination && c.StraightMoves >= minStraight
        );
    }
}

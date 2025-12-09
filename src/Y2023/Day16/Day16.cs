namespace AoC.Y2023.Day16;

public class Day16 : IAoCRunner<char[][], int>
{
    public char[][] ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(y => y.ToCharArray()).ToArray();

    public int RunPart1(
        char[][] input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => CountEnergized(input, new Beam(new Coord(0, 0), Beam.East));

    public int RunPart2(
        char[][] input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var maxY = input.Length;
        var maxX = input[0].Length;

        return Enumerable
            .Range(0, maxX)
            .Select(x => new Beam(new Coord(x, 0), Beam.South))
            .Concat(
                Enumerable.Range(0, maxX).Select(x => new Beam(new Coord(x, maxY - 1), Beam.North))
            )
            .Concat(Enumerable.Range(0, maxY).Select(y => new Beam(new Coord(0, y), Beam.East)))
            .Concat(
                Enumerable.Range(0, maxY).Select(y => new Beam(new Coord(maxX - 1, y), Beam.West))
            )
            .Max(b => CountEnergized(input, b));
    }

    static int CountEnergized(char[][] map, Beam start)
    {
        var energized = new System.Collections.Generic.HashSet<Coord>();

        var beams = new Queue<Beam>();
        beams.Enqueue(start);

        var loopDetector = new System.Collections.Generic.HashSet<Beam>();

        var maxY = map.Length;
        var maxX = map[0].Length;

        while (beams.TryDequeue(out var currentBeam))
        {
            if (
                0 <= currentBeam.Current.X
                && currentBeam.Current.X < maxX
                && 0 <= currentBeam.Current.Y
                && currentBeam.Current.Y < maxY
            )
            {
                energized.Add(currentBeam.Current);

                foreach (
                    var next in currentBeam.Advance(
                        map[currentBeam.Current.Y][currentBeam.Current.X]
                    )
                )
                {
                    if (loopDetector.Add(next))
                    {
                        beams.Enqueue(next);
                    }
                }
            }
        }

        return energized.Count;
    }
}

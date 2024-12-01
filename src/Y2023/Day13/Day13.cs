namespace AoC.Y2023.Day13;

public class Day13 : IAoCRunner<IReadOnlyList<IReadOnlyDictionary<Coord, char>>, long>
{
    public IReadOnlyList<IReadOnlyDictionary<Coord, char>> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Split(l => l.Length == 0)
        .Select(m => m
            .Index()
            .SelectMany(y => y.Item.Select((c, x) => (new Coord(x, y.Index), c)))
            .ToDictionary())
        .ToList();

    public long RunPart1(IReadOnlyList<IReadOnlyDictionary<Coord, char>> input) => input
        .Select(m => FindReflection(m, 0))
        .Sum();

    public long RunPart2(IReadOnlyList<IReadOnlyDictionary<Coord, char>> input) => input
        .Select(m => FindReflection(m, 1))
        .Sum();

    static readonly Coord Right = new(1, 0);
    static readonly Coord Below = new(0, 1);

    static long FindReflection(IReadOnlyDictionary<Coord, char> map, int allowedSmudges) => (
        from d in new[] { Right, Below }
        from m in GetLine(map, d, d)
        where NotReflected(map, m, d) == allowedSmudges
        select m.X + (100 * m.Y)
    ).First();

    static IEnumerable<Coord> GetLine(IReadOnlyDictionary<Coord, char> map, Coord begin, Coord direction)
    {
        for (var current = begin; map.ContainsKey(current); current += direction)
        {
            yield return current;
        }
    }

    static int NotReflected(IReadOnlyDictionary<Coord, char> map, Coord mirror, Coord direction) => (
        from line in GetLine(map, mirror, Opposite(direction))
        let mirrorsA = GetLine(map, line, direction)
        let mirrorsB = GetLine(map, line - direction, -direction)
        select mirrorsA.Zip(mirrorsB).Count(t => map[t.Item1] != map[t.Item2])
    ).Sum();

    static Coord Opposite(Coord direction) => direction == Right
        ? Below
        : Right;
}

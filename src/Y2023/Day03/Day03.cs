namespace AoC.Y2023.Day03;

public record Number(int StartX, int EndX, int Y, int Value);
public record Symbol(Coord Coord, char Value);

public partial class Day03 : IAoCRunner<(IReadOnlyList<Number> numbers, IReadOnlyList<Symbol> symbols), int>
{
    [GeneratedRegex(@"(\d+)|([^.])")]
    private static partial Regex Schematic();

    public (IReadOnlyList<Number>, IReadOnlyList<Symbol>) ParseInput(IEnumerable<string> puzzleInput)
    {
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();

        var y = 0;

        foreach (var line in puzzleInput)
        {
            var matches = Schematic().Matches(line);

            foreach (Match match in matches)
            {
                if (match.Groups[1].Success)
                {
                    var group = match.Groups[1];
                    var x1 = group.Index;
                    var x2 = x1 + group.Length - 1;
                    var value = int.Parse(group.Value, CultureInfo.CurrentCulture);

                    numbers.Add(new Number(x1, x2, y, value));
                }
                else
                {
                    var group = match.Groups[2];
                    var x = group.Index;
                    var value = group.Value[0];

                    symbols.Add(new Symbol(new Coord(x, y), value));
                }
            }

            y++;
        }

        return (numbers, symbols);
    }

    public int RunPart1((IReadOnlyList<Number> numbers, IReadOnlyList<Symbol> symbols) input)
    {
        var symbolCoords = input
            .symbols
            .Select(s => s.Coord)
            .SelectMany(c => c.Adjacent())
            .ToHashSet();

        return input
            .numbers
            .Where(n => SuperEnumerable
                .Sequence(n.StartX, n.EndX)
                .Select(x => new Coord(x, n.Y))
                .Any(c => symbolCoords.Contains(c)))
            .Sum(n => n.Value);
    }

    public int RunPart2((IReadOnlyList<Number> numbers, IReadOnlyList<Symbol> symbols) input)
    {
        var gears = input
            .symbols
            .Where(s => s.Value == '*')
            .Select(g => g.Coord)
            .ToImmutableHashSet();

        return input
            .numbers
            .Choose(n =>
            {
                var adj = SuperEnumerable
                    .Sequence(n.StartX, n.EndX)
                    .Select(x => new Coord(x, n.Y))
                    .SelectMany(c => c.Adjacent())
                    .ToImmutableHashSet();

                var intersect = adj.Intersect(gears);

                return intersect.Count == 0
                    ? (false, System.Array.Empty<(int number, Coord gear)>())
                    : (true, intersect.Select(g => (number: n.Value, gear: g)).ToArray());
            })
            .SelectMany(g => g)
            .GroupBy(g => g.gear, g => g.number)
            .ToDictionary(g => g.Key, g => g.ToArray())
            .Choose(kvp => (kvp.Value.Length == 2, kvp.Value.Product()))
            .Sum();
    }
}

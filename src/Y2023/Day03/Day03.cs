namespace AoC.Y2023.Day03;

public record Number(int StartX, int EndX, int Y, int Value);
public record Symbol(Coord Coord, char Value);
public record Parsed(ImmutableList<Number> Numbers, ImmutableList<Symbol> Symbols)
    : IMonoid<Parsed>
{
    public static Parsed Empty() => new(ImmutableList<Number>.Empty, ImmutableList<Symbol>.Empty);

    public Parsed Append(Parsed other) => new(
        this.Numbers.AddRange(other.Numbers),
        this.Symbols.AddRange(other.Symbols));
}

public partial class Day03 : IAoCRunner<Parsed, int>
{
    [GeneratedRegex(@"(\d+)|([^.])")]
    private static partial Regex Schematic();

    public Parsed ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Index()
        .Aggregate(
            Parsed.Empty(),
            static (result, current) => result.Append(Schematic()
                .Matches(current.item)
                .Partition(
                    static m => m.Groups[1].Success,
                    (ns, ss) =>
                    {
                        var numbers = ns
                            .Select(n =>
                            {
                                var group = n.Groups[1];
                                var x1 = group.Index;
                                var x2 = x1 + group.Length - 1;
                                var value = int.Parse(group.Value, CultureInfo.CurrentCulture);

                                return new Number(x1, x2, current.index, value);
                            })
                            .ToImmutableList();

                        var symbols = ss
                            .Select(s =>
                            {
                                var group = s.Groups[2];
                                var x = group.Index;
                                var value = group.Value[0];

                                return new Symbol(new Coord(x, current.index), value);
                            })
                            .ToImmutableList();

                        return new Parsed(numbers, symbols);
                    })));

    public int RunPart1(Parsed input)
    {
        var symbolCoords = input
            .Symbols
            .Select(s => s.Coord)
            .SelectMany(c => c.Adjacent())
            .ToHashSet();

        return input
            .Numbers
            .Where(n => SuperEnumerable
                .Sequence(n.StartX, n.EndX)
                .Select(x => new Coord(x, n.Y))
                .Any(c => symbolCoords.Contains(c)))
            .Sum(n => n.Value);
    }

    public int RunPart2(Parsed input)
    {
        var gears = input
            .Symbols
            .Where(s => s.Value == '*')
            .Select(g => g.Coord)
            .ToImmutableHashSet();

        return input
            .Numbers
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

namespace AoC.Y2023.Day03;

public partial class Day03 : IAoCRunner<Parsed, int>
{
    [GeneratedRegex(
        @"(?<number>\d+)|(?<symbol>[^.])",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private static partial Regex Schematic();

    public Parsed ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .Aggregate(
                Parsed.Empty(),
                static (result, current) =>
                    result.Append(
                        Schematic()
                            .Matches(current.Item)
                            .Partition(
                                static m => m.Groups["number"].Success,
                                (ns, ss) =>
                                {
                                    var numbers = ns.Select(n =>
                                        {
                                            var group = n.Groups["number"];
                                            var x1 = group.Index;
                                            var x2 = x1 + group.Length - 1;
                                            var value = int.Parse(
                                                group.Value,
                                                CultureInfo.CurrentCulture
                                            );

                                            return new Number(x1, x2, current.Index, value);
                                        })
                                        .ToImmutableList();

                                    var symbols = ss.Select(s =>
                                        {
                                            var group = s.Groups["symbol"];
                                            var x = group.Index;
                                            var value = group.Value[0];

                                            return new Symbol(new Coord(x, current.Index), value);
                                        })
                                        .ToImmutableList();

                                    return new Parsed(numbers, symbols);
                                }
                            )
                    )
            );

    public int RunPart1(
        Parsed input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var symbolCoords = input
            .Symbols.Select(s => s.Coord)
            .SelectMany(c => c.Adjacent())
            .ToHashSet();

        return input
            .Numbers.Where(n =>
                SuperEnumerable
                    .Sequence(n.StartX, n.EndX)
                    .Select(x => new Coord(x, n.Y))
                    .Any(c => symbolCoords.Contains(c))
            )
            .Sum(n => n.Value);
    }

    public int RunPart2(
        Parsed input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var gears = input
            .Symbols.Where(s => s.Value == '*')
            .Select(g => g.Coord)
            .ToImmutableHashSet();

        return input
            .Numbers.Choose(n =>
            {
                var adj = SuperEnumerable
                    .Sequence(n.StartX, n.EndX)
                    .Select(x => new Coord(x, n.Y))
                    .SelectMany(c => c.Adjacent())
                    .ToImmutableHashSet();

                var intersect = adj.Intersect(gears);

                return intersect.Count == 0
                    ? (false, [])
                    : (true, intersect.Select(g => (number: n.Value, gear: g)).ToArray());
            })
            .SelectMany(g => g)
            .GroupBy(g => g.gear, g => g.number)
            .ToDictionary(g => g.Key, g => g.ToArray())
            .Choose(kvp => (kvp.Value.Length == 2, kvp.Value.Product()))
            .Sum();
    }
}

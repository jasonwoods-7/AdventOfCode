using ParsedInput = System.Collections.Generic.IReadOnlyDictionary<
    char,
    System.Collections.Generic.IReadOnlyList<AoC.Types.Coord>
>;

namespace AoC.Y2024.Day08;

public class Day08 : IAoCRunner<ParsedInput, int>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .SelectMany(l => l.Item.Index(), (l, c) => (x: c.Index, y: l.Index, c: c.Item))
            .GroupBy(t => t.c, t => new Coord(t.x, t.y))
            .ToDictionary(g => g.Key, IReadOnlyList<Coord> (g) => g.ToList());

    public int RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var bounds = Bounds.FindBounds(input.Values.SelectMany(c => c));

        return input
            .Choose(kvp => (kvp.Key != '.', kvp.Value))
            .SelectMany(
                v => v.Subsets(2),
                (_, s) =>
                    s.Fold(
                        (x, y) =>
                        {
                            var slope = x - y;
                            return new[] { x + slope, y - slope };
                        }
                    )
            )
            .SelectMany(c => c)
            .ToHashSet()
            .Count(c => bounds.WithinBounds(c));
    }

    public int RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var bounds = Bounds.FindBounds(input.Values.SelectMany(c => c));

        return input
            .Choose(kvp => (kvp.Key != '.', kvp.Value))
            .SelectMany(
                v => v.Subsets(2),
                (_, s) =>
                    s.Fold(
                        (x, y) =>
                        {
                            var slope = x - y;
                            var result = new List<Coord> { x, y };

                            var next = x + slope;
                            while (bounds.WithinBounds(next))
                            {
                                result.Add(next);
                                next += slope;
                            }

                            next = y - slope;
                            while (bounds.WithinBounds(next))
                            {
                                result.Add(next);
                                next -= slope;
                            }

                            return result;
                        }
                    )
            )
            .SelectMany(c => c)
            .ToHashSet()
            .Count;
    }
}

using ParsedInput = System.Collections.Immutable.ImmutableDictionary<AoC.Types.Coord, char>;

namespace AoC.Y2025.Day07;

public class Day07 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Index()
            .SelectMany(t => t.Item.Index(), (t, c) => (new Coord(c.Index, t.Index), c.Item))
            .Where(t => t.Item != '.')
            .ToImmutableDictionary(t => t.Item1, t => t.Item);

    public long RunPart1(ParsedInput input)
    {
        var start = input.Single(kvp => kvp.Value == 'S').Key;

        var height = input.Max(kvp => kvp.Key.Y);

        var beams = ImmutableHashSet<Coord>.Empty.Add(start.Below());

        var splits = 0;

        for (var counter = 1; counter <= height; ++counter)
        {
            var nextBeams = ImmutableHashSet<Coord>.Empty;

            foreach (var beam in beams)
            {
                if (input.ContainsKey(beam))
                {
                    nextBeams = nextBeams.AddRange([beam.BelowLeft(), beam.BelowRight()]);
                    splits++;
                }
                else
                {
                    nextBeams = nextBeams.Add(beam.Below());
                }
            }

            beams = nextBeams;
        }

        return splits;
    }

    public long RunPart2(ParsedInput input)
    {
        var start = input.Single(kvp => kvp.Value == 'S').Key;
        var cache = new Dictionary<Coord, long>();
        var height = input.Max(kvp => kvp.Key.Y);

        return Part2Solver(input, start.Below(), cache, height);
    }

    static long Part2Solver(
        ParsedInput input,
        Coord currentPos,
        Dictionary<Coord, long> cache,
        long height
    )
    {
        if (height == 0)
        {
            return 1;
        }

        if (cache.TryGetValue(currentPos, out var value))
        {
            return value;
        }

        var result = input.ContainsKey(currentPos)
            ? Part2Solver(input, currentPos.BelowLeft(), cache, height - 1)
                + Part2Solver(input, currentPos.BelowRight(), cache, height - 1)
            : Part2Solver(input, currentPos.Below(), cache, height - 1);

        cache[currentPos] = result;

        return result;
    }
}

using ParsedInput = System.Collections.Immutable.ImmutableDictionary<AoC.Types.Coord, char>;
using Visited = System.Collections.Generic.HashSet<AoC.Types.Coord>;
using LoopDetector = System.Collections.Generic.HashSet<(AoC.Types.Coord, AoC.Types.Coord)>;

namespace AoC.Y2024.Day06;

public class Day06 : IAoCRunner<ParsedInput, int>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Index()
        .SelectMany(l => l.Item.Index(), (l, c) => (x: c.Index, y: l.Index, c: c.Item))
        .Where(t => t.c != '.')
        .ToImmutableDictionary(t => new Coord(t.x, t.y), t => t.c);

    public int RunPart1(ParsedInput input)
    {
        var initial = input.Single(kvp => kvp.Value == '^').Key;
        input = input.Remove(initial);

        var bounds = Bounds.FindBounds(input.Keys);

        var (_, visited) = DetectLoop(input, bounds, initial);

        return visited.Count;
    }

    public int RunPart2(ParsedInput input)
    {
        var initial = input.Single(kvp => kvp.Value == '^').Key;
        input = input.Remove(initial);

        var bounds = Bounds.FindBounds(input.Keys);

        var (_, visited) = DetectLoop(input, bounds, initial);

        return visited
            .FluentRemove(initial)
            .Count(c => DetectLoop(input.Add(c, '#'), bounds, initial).Item1);
    }

    static (bool, Visited) DetectLoop(ParsedInput input, Bounds bounds, Coord initial)
    {
        var current = initial;
        var currentDirection = Coord.Up;

        var visited = new Visited();
        var loopDetector = new LoopDetector();

        while (bounds.WithinBounds(current))
        {
            visited.Add(current);

            var next = current + currentDirection;
            if (input.ContainsKey(next))
            {
                if (!loopDetector.Add((current, currentDirection)))
                {
                    return (true, visited);
                }

                currentDirection = currentDirection.TurnRight();
            }
            else
            {
                current = next;
            }
        }

        return (false, visited);
    }
}

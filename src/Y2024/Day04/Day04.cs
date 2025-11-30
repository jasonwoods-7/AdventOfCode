using ParsedInput = CommunityToolkit.HighPerformance.ReadOnlyMemory2D<char>;

namespace AoC.Y2024.Day04;

public class Day04 : IAoCRunner<ParsedInput, int>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput)
    {
        var input = puzzleInput.ToList();

        return input
            .SelectMany(c => c)
            .ToArray()
            .Apply(a => new ParsedInput(a, input.Count, input.First().Length));
    }

    public int RunPart1(ParsedInput input)
    {
        var xmasCount = 0;

        var bounds = new Bounds((0, 0), (input.Width - 1, input.Height - 1));
        var xmasSearch = ImmutableQueue.Create('M', 'A', 'S');

        var searchDirections = Coord.Zero.Adjacent().ToArray();

        for (var y = 0; y < input.Height; ++y)
        {
            for (var x = 0; x < input.Width; ++x)
            {
                var coord = new Coord(x, y);

                if (input.Span.At(coord) == 'X')
                {
                    xmasCount += searchDirections.Count(d =>
                        FindXmas(input, coord + d, d, bounds, xmasSearch)
                    );
                }
            }
        }

        return xmasCount;
    }

    public int RunPart2(ParsedInput input)
    {
        var xedMasCount = 0;
        var puzzle = input.Span;

        for (var y = 1; y < input.Height - 1; ++y)
        {
            for (var x = 1; x < input.Width - 1; ++x)
            {
                var coord = new Coord(x, y);

                if (puzzle.At(coord) == 'A')
                {
                    xedMasCount += (
                        puzzle.At(coord.AboveLeft()),
                        puzzle.At(coord.BelowRight()),
                        puzzle.At(coord.AboveRight()),
                        puzzle.At(coord.BelowLeft())
                    ) switch
                    {
                        ('M', 'S', 'M', 'S') => 1,
                        ('M', 'S', 'S', 'M') => 1,
                        ('S', 'M', 'M', 'S') => 1,
                        ('S', 'M', 'S', 'M') => 1,
                        _ => 0,
                    };
                }
            }
        }

        return xedMasCount;
    }

    static bool FindXmas(
        ParsedInput puzzle,
        Coord current,
        Coord increment,
        Bounds bounds,
        ImmutableQueue<char> search
    )
    {
        if (search.IsEmpty)
        {
            return true;
        }

        if (!bounds.WithinBounds(current))
        {
            return false;
        }

        var rest = search.Dequeue(out var value);

        if (puzzle.Span.At(current) != value)
        {
            return false;
        }

        return FindXmas(puzzle, current + increment, increment, bounds, rest);
    }
}

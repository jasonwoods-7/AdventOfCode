namespace AoC.Y2022.Day17;

partial class Day17
{
    static IEnumerable<IReadOnlyList<Coord>> Shapes()
    {
        var horizontalLine = CreateShape([
            "####"
        ]);

        var plus = CreateShape([
            " # ",
            "###",
            " # "
        ]);

        var backwardsL = CreateShape([
            "  #",
            "  #",
            "###"
        ]);

        var verticalLine = CreateShape([
            "#",
            "#",
            "#",
            "#"
        ]);

        var block = CreateShape([
            "##",
            "##"
        ]);

        while (true)
        {
            yield return horizontalLine;
            yield return plus;
            yield return backwardsL;
            yield return verticalLine;
            yield return block;
        }

        // ReSharper disable once IteratorNeverReturns
    }

    static IReadOnlyList<Coord> CreateShape(string[] representation)
    {
        var coords = new List<Coord>();

        var reversed = representation.Reverse().Index();

        foreach (var (y, line) in reversed)
        {
            for (var x = 0; x < line.Length; ++x)
            {
                if (line[x] == '#')
                {
                    coords.Add(new Coord(x + 2, -4 - y));
                }
            }
        }

        return coords;
    }
}

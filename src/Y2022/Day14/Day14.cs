namespace AoC.Y2022.Day14;

public class Day14 : IAoCRunner<Dictionary<Coord, char>, int>
{
    static readonly Coord InitialCoord = new(500, 0);

    public Dictionary<Coord, char> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(static l =>
                l.Split(" -> ")
                    .Select(static p =>
                        p.Split(",")
                            .Fold(
                                static (x, y) =>
                                    new Coord(
                                        int.Parse(x, CultureInfo.CurrentCulture),
                                        int.Parse(y, CultureInfo.CurrentCulture)
                                    )
                            )
                    )
            )
            .SelectMany(static cs =>
                cs.Window(2)
                    .SelectMany(static ws =>
                        ws.Fold(
                            static (w1, w2) =>
                            {
                                Debug.Assert(w1.X == w2.X || w1.Y == w2.Y);

                                var delta = new Coord(
                                    Math.Sign(w2.X - w1.X),
                                    Math.Sign(w2.Y - w1.Y)
                                );

                                return EnumerableExtensions.Range(w1, w2 + delta, c => c + delta);
                            }
                        )
                    )
            )
            .Distinct()
            .ToDictionary(c => c, _ => '#');

    public int RunPart1(Dictionary<Coord, char> input)
    {
        // ReSharper disable ConvertToLocalFunction
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> isOccupied = static (c, a) =>
            a.ContainsKey(c);

        var maxY = input.Keys.MaxBy(static c => c.Y).Y;
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> exitCondition = (c, _) => c.Y > maxY;
        // ReSharper restore ConvertToLocalFunction

        return GetSandCount(input, isOccupied, exitCondition);
    }

    public int RunPart2(Dictionary<Coord, char> input)
    {
        // ReSharper disable ConvertToLocalFunction
        var maxY = input.Keys.MaxBy(static c => c.Y).Y;
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> isOccupied = (c, a) =>
            a.ContainsKey(c) || c.Y == maxY + 2;

        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> exitCondition = static (c, a) =>
            c == InitialCoord && a.ContainsKey(c);
        // ReSharper restore ConvertToLocalFunction

        return GetSandCount(input, isOccupied, exitCondition);
    }

    static int GetSandCount(
        Dictionary<Coord, char> caveMap,
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> isOccupied,
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> exitCondition
    )
    {
        while (
            FindRestingPosition(InitialCoord, caveMap, isOccupied, exitCondition)
                is { IsSome: true } restingPosition
        )
        {
            caveMap[restingPosition.IfNone(Coord.Zero)] = 'o';
        }

        return caveMap.Count(c => c.Value == 'o');
    }

    static Option<Coord> FindRestingPosition(
        Coord current,
        IReadOnlyDictionary<Coord, char> currentlyOccupied,
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> isOccupied,
        Func<Coord, IReadOnlyDictionary<Coord, char>, bool> exitCondition
    )
    {
        if (exitCondition(current, currentlyOccupied))
        {
            return None;
        }

        var below = current.Below();
        if (!isOccupied(below, currentlyOccupied))
        {
            return FindRestingPosition(below, currentlyOccupied, isOccupied, exitCondition);
        }

        var belowLeft = current.BelowLeft();
        if (!isOccupied(belowLeft, currentlyOccupied))
        {
            return FindRestingPosition(belowLeft, currentlyOccupied, isOccupied, exitCondition);
        }

        var belowRight = current.BelowRight();
        if (!isOccupied(belowRight, currentlyOccupied))
        {
            return FindRestingPosition(belowRight, currentlyOccupied, isOccupied, exitCondition);
        }

        return current;
    }
}

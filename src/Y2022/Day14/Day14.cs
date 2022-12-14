namespace AoC.Y2022.Day14;

public class Day14 : IAoCRunner<ImmutableHashSet<Coord>, int>
{
    static readonly Coord InitialCoord = new(500, 0);

    public ImmutableHashSet<Coord> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(static l => l
            .Split(" -> ")
            .Select(static p => p
                .Split(",")
                .Fold(static (x, y) => new Coord(
                    int.Parse(x, CultureInfo.CurrentCulture),
                    int.Parse(y, CultureInfo.CurrentCulture)))))
        .SelectMany(static cs => cs
            .Window(2)
            .SelectMany(ws => ws
                .Fold(static (w1, w2) =>
                {
                    Debug.Assert(w1.X == w2.X || w1.Y == w2.Y);

                    var positions = new List<Coord>();

                    if (w1.X == w2.X)
                    {
                        var minY = Math.Min(w1.Y, w2.Y);
                        var maxY = Math.Max(w1.Y, w2.Y);

                        for (var counter = minY; counter <= maxY; ++counter)
                        {
                            positions.Add(w1 with { Y = counter });
                        }
                    }
                    else
                    {
                        var minX = Math.Min(w1.X, w2.X);
                        var maxX = Math.Max(w1.X, w2.X);

                        for (var counter = minX; counter <= maxX; ++counter)
                        {
                            positions.Add(w1 with { X = counter });
                        }
                    }

                    return positions;
                })))
        .ToImmutableHashSet();

    public int RunPart1(ImmutableHashSet<Coord> input)
    {
        var sandPos = ImmutableHashSet<Coord>.Empty;

        // ReSharper disable ConvertToLocalFunction
        Func<Coord, ImmutableHashSet<Coord>, bool> isOccupied = static (c, a) => a.Contains(c);

        var maxY = input.MaxBy(static c => c.Y).Y;
        Func<Coord, ImmutableHashSet<Coord>, bool> exitCondition = (c, _) => c.Y > maxY;
        // ReSharper restore ConvertToLocalFunction

        while (FindRestingPosition(
            new Coord(500, 0),
            input.Union(sandPos),
            isOccupied,
            exitCondition) is { IsSome: true } restingPosition)
        {
            sandPos = sandPos.Add(restingPosition.IfNone(default(Coord)));
        }

        return sandPos.Count;
    }

    public int RunPart2(ImmutableHashSet<Coord> input)
    {
        var sandPos = ImmutableHashSet<Coord>.Empty;

        // ReSharper disable ConvertToLocalFunction
        var maxY = input.MaxBy(static c => c.Y).Y;

        Func<Coord, ImmutableHashSet<Coord>, bool> isOccupied = (c, a) => a.Contains(c) || c.Y == maxY + 2;

        Func<Coord, ImmutableHashSet<Coord>, bool> exitCondition = static (c, a) => c == InitialCoord && a.Contains(c);
        // ReSharper restore ConvertToLocalFunction

        while (FindRestingPosition(
            new Coord(500, 0),
            input.Union(sandPos),
            isOccupied,
            exitCondition) is { IsSome: true } restingPosition)
        {
            sandPos = sandPos.Add(restingPosition.IfNone(default(Coord)));
        }

        return sandPos.Count;
    }

    static Option<Coord> FindRestingPosition(
        Coord current,
        ImmutableHashSet<Coord> currentlyOccupied,
        Func<Coord, ImmutableHashSet<Coord>, bool> isOccupied,
        Func<Coord, ImmutableHashSet<Coord>, bool> exitCondition)
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

namespace AoC.Y2024.Day07;

public class Day07 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l =>
                l.FindNumbers<long>().ToList() switch
                {
                    [var head, .. var rest] => new Equation(head, rest),
                    _ => throw new InvalidOperationException(),
                }
            )
            .Apply(eqs => new ParsedInput(eqs.ToList()));

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solver(input, (cur, c) => [c * cur, c + cur]);

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solver(input, (cur, c) => [c * cur, c + cur, $"{c}{cur}".ParseNumber<long>()]);

    static long Solver(ParsedInput input, Func<long, long, long[]> next) =>
        input
            .Equations.Where(eq =>
                eq.Values.Tail()
                    .Aggregate(
                        new List<long> { eq.Values.Head() },
                        (res, cur) => res.SelectMany(c => next(cur, c)).ToList()
                    )
                    .Contains(eq.Result)
            )
            .Sum(eq => eq.Result);
}

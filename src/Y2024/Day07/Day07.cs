namespace AoC.Y2024.Day07;

public record ParsedInput(IReadOnlyList<Equation> Equations);
public record Equation(long Result, IReadOnlyList<long> Values);

public class Day07 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(l => l.FindNumbers<long>().ToList() switch
        {
            [var head, .. var rest] => new Equation(head, rest),
            _ => throw new InvalidOperationException()
        })
        .Apply(eqs => new ParsedInput(eqs.ToList()));

    public long RunPart1(ParsedInput input) => input
        .Equations
        .Choose(eq =>
        {
            var head = eq.Values.Head();
            var tail = eq.Values.Tail();

            var values = tail
                .Aggregate(
                    new List<long> { head },
                    (res, cur) => res
                        .SelectMany(c => new[] { c * cur, c + cur })
                        .ToList());

            return (values.Contains(eq.Result), eq.Result);
        })
        .Sum();

    public long RunPart2(ParsedInput input) => input
        .Equations
        .Choose(eq =>
        {
            var head = eq.Values.Head();
            var tail = eq.Values.Tail();

            var values = tail
                .Aggregate(
                    new List<long> { head },
                    (res, cur) => res
                        .SelectMany(c => new[] { c * cur, c + cur, $"{c}{cur}".ParseNumber<long>() })
                        .ToList());

            return (values.Contains(eq.Result), eq.Result);
        })
        .Sum();
}

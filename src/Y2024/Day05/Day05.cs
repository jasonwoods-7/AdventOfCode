namespace AoC.Y2024.Day05;

public class Day05 : IAoCRunner<ParsedInput, int>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(l => l.Length == 0)
            .Fold(
                (rules, updates) =>
                    new ParsedInput(
                        rules
                            .Select(r => r.FindNumbers<int>().Fold((b, a) => new Rule(b, a)))
                            .ToList(),
                        updates.Select(u => new Update(u.FindNumbers<int>().ToList())).ToList()
                    )
            );

    public int RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Updates.Select(u => u.Pages)
            .Where(p => InCorrectOrder(p, input.Rules))
            .Sum(p => p[p.Count / 2]);

    public int RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Updates.Select(u => u.Pages)
            .Where(p => !InCorrectOrder(p, input.Rules))
            .Select(p => p.Order(new UpdateComparer(input.Rules)).ToList())
            .Sum(p => p[p.Count / 2]);

    static bool InCorrectOrder(IReadOnlyList<int> pages, IReadOnlyList<Rule> rules) =>
        rules
            .Select(r => (b: pages.IndexOf(r.Before), a: pages.IndexOf(r.After)))
            .Where(t => t.b != -1 && t.a != -1)
            .All(t => t.b < t.a);
}

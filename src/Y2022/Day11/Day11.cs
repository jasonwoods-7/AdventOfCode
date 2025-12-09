namespace AoC.Y2022.Day11;

public class Day11 : IAoCRunner<IReadOnlyDictionary<int, Monkey>, long>
{
    public IReadOnlyDictionary<int, Monkey> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(static l => l == string.Empty)
            .Select(static ls =>
                ls.Fold(
                    static (monkeyId, startingItems, operation, test, truePart, falsePart) =>
                        new Monkey(
                            monkeyId.FindNumbers<int>().Single(),
                            startingItems.FindNumbers<long>(),
                            MonkeyParserHelpers.CreateOperationFunc(operation),
                            MonkeyParserHelpers.CreateTestFunc(
                                test.FindNumbers<long>().Single(),
                                truePart.FindNumbers<int>().Single(),
                                falsePart.FindNumbers<int>().Single()
                            ),
                            test.FindNumbers<int>().Single()
                        )
                )
            )
            .ToDictionary(static m => m.Id);

    public long RunPart1(
        IReadOnlyDictionary<int, Monkey> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => CalculateMonkeyBusiness(input, 20, static v => Math.DivRem(v, 3).Quotient);

    public long RunPart2(
        IReadOnlyDictionary<int, Monkey> input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var reducer = new Reducer(input.Aggregate(1L, (p, m) => p * m.Value.TestDiv));
        return CalculateMonkeyBusiness(input, 10_000, reducer.Reduce);
    }

    static long CalculateMonkeyBusiness(
        IReadOnlyDictionary<int, Monkey> input,
        int rounds,
        Func<long, long> worryModifier
    )
    {
        var orderedMonkeys = input.Select(m => m.Value).OrderBy(m => m.Id).ToList();

        for (var i = 0; i < rounds; ++i)
        {
            foreach (var monkey in orderedMonkeys)
            {
                foreach (var (value, id) in monkey.PerformInspections(worryModifier))
                {
                    input[id].Items.Enqueue(value);
                }
            }
        }

        return orderedMonkeys
            .Select(m => m.InspectionsCount)
            .OrderDescending()
            .Take(2)
            .Fold((m1, m2) => m1 * m2);
    }
}

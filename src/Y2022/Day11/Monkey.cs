namespace AoC.Y2022.Day11;

public class Monkey
{
    readonly Func<long, long> _operation;
    readonly Func<long, int> _test;

    public Monkey(int id, IEnumerable<long> initialItems, Func<long, long> operation, Func<long, int> test, int testDiv)
    {
        Id = id;
        Items = new Queue<long>(initialItems);
        _operation = operation;
        _test = test;
        TestDiv = testDiv;
    }

    public int Id { get; }

    public Queue<long> Items { get; }

    public int TestDiv { get; }

    public long InspectionsCount { get; private set; }

    public IEnumerable<(long value, int id)> PerformInspections(Func<long, long> worryModifier)
    {
        while (Items.TryDequeue(out var oldValue))
        {
            InspectionsCount++;

            var newValue = worryModifier(_operation(oldValue));
            var id = _test(newValue);

            yield return (newValue, id);
        }
    }
}

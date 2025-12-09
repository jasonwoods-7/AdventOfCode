namespace AoC.Y2022.Day20;

public class Day20 : IAoCRunner<IReadOnlyList<long>, long>
{
    public IReadOnlyList<long> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(l => long.Parse(l, CultureInfo.CurrentCulture)).ToList();

    public long RunPart1(
        IReadOnlyList<long> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solve(input, 1, 1);

    public long RunPart2(
        IReadOnlyList<long> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solve(input, 811_589_153, 10);

    static long Solve(IReadOnlyList<long> input, long decryptionKey, int rounds)
    {
        var length = input.Count - 1;

        var circularList = new CircularList(input.Select(i => i * decryptionKey));

        using var listEnumerator = circularList.GetEnumerator();

        while (rounds-- > 0)
        {
            while (listEnumerator.MoveNext())
            {
                listEnumerator.Current.Mix(length);
            }

            listEnumerator.Reset();
        }

        var node = circularList.Find(0);

        var sum = 0L;

        for (var i = 0; i < 3; ++i)
        {
            for (var j = 0; j < 1000; ++j, node = node.Next) { }

            sum += node.Value;
        }

        return sum;
    }
}

namespace AoC.Y2023.Day12;

public record Row(string Springs, ImmutableStack<int> Groups)
{
    public Row Unfold(int multiplier) =>
        new(
            string.Join('?', Enumerable.Repeat(Springs, multiplier)),
            Enumerable
                .Repeat(Groups, multiplier)
                .SelectMany(SuperEnumerable.Identity)
                .Reverse()
                .Apply(ImmutableStack.CreateRange)
        );
}

public class Day12 : IAoCRunner<IReadOnlyList<Row>, long>
{
    public IReadOnlyList<Row> ParseInput(IEnumerable<string> puzzleInput) =>
        (
            from line in puzzleInput
            let parts = line.Split(" ")
            let pattern = parts[0]
            let numString = parts[1]
            let nums = numString.Split(',').Select(int.Parse)
            select new Row(pattern, ImmutableStack.CreateRange(nums.Reverse()))
        ).ToList();

    public long RunPart1(IReadOnlyList<Row> input) =>
        input.Sum(r => Compute(r, new Dictionary<Row, long>()));

    public long RunPart2(IReadOnlyList<Row> input) =>
        input.Sum(r => Compute(r.Unfold(5), new Dictionary<Row, long>()));

    static long Compute(Row row, Dictionary<Row, long> cache)
    {
        if (cache.TryGetValue(row, out var value))
        {
            return value;
        }

        value = Dispatch(row, cache);
        cache[row] = value;

        return value;
    }

    static long Dispatch(Row row, Dictionary<Row, long> cache) =>
        row.Springs.FirstOrDefault() switch
        {
            '.' => ProcessDot(row, cache),
            '?' => ProcessQuestion(row, cache),
            '#' => ProcessHash(row, cache),
            _ => ProcessEnd(row.Groups),
        };

    static long ProcessEnd(ImmutableStack<int> nums) => !nums.IsEmpty ? 0 : 1;

    static long ProcessDot(Row row, Dictionary<Row, long> cache) =>
        Compute(row with { Springs = row.Springs[1..] }, cache);

    static long ProcessQuestion(Row row, Dictionary<Row, long> cache) =>
        Compute(row with { Springs = "." + row.Springs[1..] }, cache)
        + Compute(row with { Springs = "#" + row.Springs[1..] }, cache);

    static long ProcessHash(Row row, Dictionary<Row, long> cache)
    {
        if (row.Groups.IsEmpty)
        {
            return 0;
        }

        var next = row.Groups.Pop(out var n);

        var potentiallyDead = row.Springs.TakeWhile(s => s is '#' or '?').Count();

        return potentiallyDead < n ? 0
            : row.Springs.Length == n ? Compute(new Row("", next), cache)
            : row.Springs[n] == '#' ? 0
            : Compute(new Row(row.Springs[(n + 1)..], next), cache);
    }
}

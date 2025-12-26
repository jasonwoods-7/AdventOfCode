namespace AoC.Y2023.Day15;

public partial class Day15 : IAoCRunner<IEnumerable<string>, int>
{
    [GeneratedRegex(
        @"(?<label>\w+)(?:(?<lens>=\d)|(-))",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private partial Regex OperationRegex();

    public IEnumerable<string> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.SelectMany(l => l.Split(','));

    public int RunPart1(
        IEnumerable<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Sum(Hash);

    public int RunPart2(
        IEnumerable<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var boxes = Enumerable.Range(0, 256).ToDictionary(i => i, _ => new List<Lens>());

        foreach (var current in input)
        {
            var match = OperationRegex().Match(current);

            var label = match.Groups["label"].Value;
            var hash = Hash(label);

            if (match.Groups["lens"].Success)
            {
                AddLens(match.Groups["lens"].Value[1..].ParseNumber<int>(), boxes[hash], label);
            }
            else
            {
                boxes[hash].RemoveAll(l => string.Equals(l.Label, label, StringComparison.Ordinal));
            }
        }

        return boxes.Sum(b => b.Value.Index(1).Sum(t => (b.Key + 1) * t.Index * t.Item.Value));
    }

    static int Hash(string label) =>
        label.Aggregate(0, (result, current) => ((result + current) * 17) & 0xFF);

    static void AddLens(int lens, List<Lens> box, string label)
    {
        var existing = box.FindIndex(t => string.Equals(t.Label, label, StringComparison.Ordinal));
        if (existing != -1)
        {
            box[existing] = new Lens(label, lens);
        }
        else
        {
            box.Add(new Lens(label, lens));
        }
    }
}

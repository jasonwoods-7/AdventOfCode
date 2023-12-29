namespace AoC.Y2023.Day15;

sealed record Lens(string Label, int Value);

public partial class Day15 : IAoCRunner<IEnumerable<string>, int>
{
    [GeneratedRegex(@"(\w+)(?:(=\d)|(-))")]
    private partial Regex OperationRegex();

    public IEnumerable<string> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .SelectMany(l => l.Split(','));

    public int RunPart1(IEnumerable<string> input) => input
        .Sum(Hash);

    public int RunPart2(IEnumerable<string> input)
    {
        var boxes = Enumerable
            .Range(0, 256)
            .ToDictionary(i => i, _ => new List<Lens>());

        foreach (var current in input)
        {
            var match = OperationRegex().Match(current);

            var label = match.Groups[1].Value;
            var hash = Hash(label);

            if (match.Groups[2].Success)
            {
                AddLens(match.Groups[2].Value[1..].ParseNumber<int>(), boxes[hash], label);
            }
            else
            {
                boxes[hash].RemoveAll(l => l.Label == label);
            }
        }

        return boxes
            .Sum(b => b.Value.Index(1).Sum(t => (b.Key + 1) * t.index * t.item.Value));
    }

    static int Hash(string label) => label.Aggregate(0, (result, current) => ((result + current) * 17) & 0xFF);

    static void AddLens(int lens, List<Lens> box, string label)
    {
        var existing = box.FindIndex(t => t.Label == label);
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

namespace AoC.Y2023.Day15;

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
        var boxes = new Dictionary<int, List<(string, int)>>();

        foreach (var current in input)
        {
            var match = OperationRegex().Match(current);

            var label = match.Groups[1].Value;
            var hash = Hash(label);

            if (match.Groups[2].Success)
            {
                AddLens(match.Groups[2].Value[1..].ParseNumber<int>(), boxes, hash, label);
            }
            else
            {
                if (boxes.TryGetValue(hash, out var value))
                {
                    value.RemoveAll(t => t.Item1 == label);
                }
            }
        }

        return boxes
            .Sum(b => b.Value.Index(1).Sum(t => (b.Key + 1) * t.index * t.item.Item2));
    }

    static int Hash(string label) => label.Aggregate(0, (result, current) => ((result + current) * 17) & 0xFF);

    static void AddLens(int lens, Dictionary<int, List<(string, int)>> boxes, int hash, string label)
    {
        if (boxes.TryGetValue(hash, out var value))
        {
            var existing = value.FindIndex(t => t.Item1 == label);
            if (existing != -1)
            {
                value[existing] = (label, lens);
            }
            else
            {
                value.Add((label, lens));
            }
        }
        else
        {
            boxes.Add(hash, [(label, lens)]);
        }
    }
}

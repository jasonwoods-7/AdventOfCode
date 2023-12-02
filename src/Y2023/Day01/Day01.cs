namespace AoC.Y2023.Day01;

public partial class Day01 : IAoCRunner<IReadOnlyList<string>, int>
{
    public IReadOnlyList<string> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput.ToList();

    public int RunPart1(IReadOnlyList<string> input) => input
        .Sum(static l => l
            .Choose(static c => (char.IsDigit(c), c - '0'))
            .ToList()
            .Apply(static d => (d[0] * 10) + d[^1]));

    [GeneratedRegex("[1-9]|(?=(one|two|three|four|five|six|seven|eight|nine))")]
    private static partial Regex NumbersRegex();

    public int RunPart2(IReadOnlyList<string> input) => input
        .Sum(static l => NumbersRegex()
            .Matches(l)
            .Choose(static m => m.Groups.OfType<Group>().Last(g => g.Success).Value switch
            {
                "1" or "one" => (true, 1),
                "2" or "two" => (true, 2),
                "3" or "three" => (true, 3),
                "4" or "four" => (true, 4),
                "5" or "five" => (true, 5),
                "6" or "six" => (true, 6),
                "7" or "seven" => (true, 7),
                "8" or "eight" => (true, 8),
                "9" or "nine" => (true, 9),
                _ => (false, 0)
            })
            .ToList()
            .Apply(static d => (d[0] * 10) + d[^1]));
}

namespace AoC.Y2023.Day01;

public class Day01 : IAoCRunner<IReadOnlyList<string>, int>
{
    public IReadOnlyList<string> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.ToList();

    public int RunPart1(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solve(input, @"\d");

    public int RunPart2(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => Solve(input, @"\d|one|two|three|four|five|six|seven|eight|nine");

    static int Solve(IReadOnlyList<string> input, string regEx) =>
        (
            from l in input
            let f = Regex.Match(l, regEx, RegexOptions.None, TimeSpan.FromSeconds(1))
            let t = Regex.Match(l, regEx, RegexOptions.RightToLeft, TimeSpan.FromSeconds(1))
            select (ParseValue(f.Value) * 10) + ParseValue(t.Value)
        ).Sum();

    static int ParseValue(string match) =>
        match switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => int.Parse(match, CultureInfo.CurrentCulture),
        };
}

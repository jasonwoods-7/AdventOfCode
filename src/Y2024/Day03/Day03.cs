namespace AoC.Y2024.Day03;

public partial class Day03 : IAoCRunner<IReadOnlyList<string>, int>
{
    [GeneratedRegex(
        @"mul\((?<ins1>\d{1,3}),(?<ins2>\d{1,3})\)",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private partial Regex Part1Instructions();

    [GeneratedRegex(
        @"mul\((?<ins1>\d{1,3}),(?<ins2>\d{1,3})\)|do(?:n't)?\(\)",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private partial Regex Part2Instructions();

    public IReadOnlyList<string> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.ToList();

    public int RunPart1(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.SelectMany(s => Part1Instructions().Matches(s), (_, m) => MultiplyValues(m)).Sum();

    public int RunPart2(
        IReadOnlyList<string> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .SelectMany(s => Part2Instructions().Matches(s))
            .Aggregate(
                (true, 0),
                (result, current) =>
                    current.Value switch
                    {
                        "do()" => (true, result.Item2),
                        "don't()" => (false, result.Item2),
                        _ when result.Item1 => (true, result.Item2 + MultiplyValues(current)),
                        _ => result,
                    },
                result => result.Item2
            );

    static int MultiplyValues(Match match) =>
        match.Groups["ins1"].Value.ParseNumber<int>()
        * match.Groups["ins2"].Value.ParseNumber<int>();
}

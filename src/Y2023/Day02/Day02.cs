namespace AoC.Y2023.Day02;

public partial class Day02 : IAoCRunner<IReadOnlyList<Game>, int>
{
    [GeneratedRegex(
        @"(?<count>\d+) (?<color>red|green|blue)",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private static partial Regex Pull();

    public IReadOnlyList<Game> ParseInput(IEnumerable<string> puzzleInput) =>
        (
            from current in puzzleInput
            select current.Split(':', ';') into parts
            select new Game(
                parts[0].FindNumbers<int>().First(),
                parts
                    .Skip(1)
                    .SelectMany(
                        static r => Pull().Matches(r),
                        static (_, m) =>
                            new Round(
                                int.Parse(m.Groups["count"].Value, CultureInfo.CurrentCulture),
                                m.Groups["color"].Value
                            )
                    )
                    .ToList()
            )
        ).ToList();

    public int RunPart1(
        IReadOnlyList<Game> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Where(static g =>
                g.Rounds.All(static r =>
                    r switch
                    {
                        (<= 12, "red") => true,
                        (<= 13, "green") => true,
                        (<= 14, "blue") => true,
                        _ => false,
                    }
                )
            )
            .Sum(g => g.Id);

    public int RunPart2(
        IReadOnlyList<Game> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Sum(g =>
        {
            var cubes = new Dictionary<string, int>(StringComparer.Ordinal)
            {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0,
            };

            foreach (var round in g.Rounds)
            {
                cubes[round.Color] = Math.Max(round.Count, cubes[round.Color]);
            }

            return cubes.Values.Product();
        });
}

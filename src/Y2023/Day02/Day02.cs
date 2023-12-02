namespace AoC.Y2023.Day02;

public record Round(int Count, string Color);
public record Game(int Id, IReadOnlyList<Round> Rounds);

public partial class Day02 : IAoCRunner<IReadOnlyList<Game>, int>
{
    [GeneratedRegex(@"(\d+) (red|green|blue)")]
    private static partial Regex Pull();

    public IReadOnlyList<Game> ParseInput(IEnumerable<string> puzzleInput) => (
        from current in puzzleInput
        select current.Split(':', ';')
        into parts
        select new Game(
            parts[0].FindNumbers<int>().First(),
            parts
                .Skip(1)
                .SelectMany(
                    static r => Pull().Matches(r),
                    static (_, m) => new Round(int.Parse(m.Groups[1].Value, CultureInfo.CurrentCulture), m.Groups[2].Value))
                .ToList())
        ).ToList();

    public int RunPart1(IReadOnlyList<Game> input) => input
        .Where(static g => g.Rounds.All(static r => r switch
            {
                (<= 12, "red") => true,
                (<= 13, "green") => true,
                (<= 14, "blue") => true,
                _ => false
            })
        )
        .Sum(g => g.Id);

    public int RunPart2(IReadOnlyList<Game> input) => input
        .Sum(g =>
        {
            var cubes = new Dictionary<string, int>
            {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0
            };

            foreach (var round in g.Rounds)
            {
                cubes[round.Color] = Math.Max(round.Count, cubes[round.Color]);
            }

            return cubes.Values.Product();
        });
}

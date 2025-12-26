namespace AoC.Y2023.Day18;

public partial class Day18 : IAoCRunner<IReadOnlyList<DigInstruction>, long>
{
    [GeneratedRegex(
        @"(?<direction>[ULDR]) (?<steps>\d+) \(#(?<color>[0-9a-f]{6})\)",
        RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 1_000
    )]
    private static partial Regex InstructionParser();

    public IReadOnlyList<DigInstruction> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(static l =>
                InstructionParser()
                    .Match(l)
                    .Apply(static m => new DigInstruction(
                        m.Groups["direction"].Value[0] switch
                        {
                            'U' => Coord.Up,
                            'L' => Coord.Left,
                            'D' => Coord.Down,
                            'R' => Coord.Right,
                            _ => throw new InvalidOperationException(),
                        },
                        m.Groups["steps"].Value.ParseNumber<int>(),
                        int.Parse(
                            m.Groups["color"].Value,
                            NumberStyles.HexNumber,
                            CultureInfo.CurrentCulture
                        )
                    ))
            )
            .ToList();

    public long RunPart1(
        IReadOnlyList<DigInstruction> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => CountInterior(input);

    public long RunPart2(
        IReadOnlyList<DigInstruction> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Select(i => i.Swap()).Apply(CountInterior);

    static long CountInterior(IEnumerable<DigInstruction> instructions) =>
        instructions.Aggregate(
            (coord: new Coord(0, 0), perimiter: 0L, area: 0L),
            static (result, current) =>
            {
                var next =
                    result.coord
                    + (current.Direction.X * current.Steps, current.Direction.Y * current.Steps);

                return (
                    next,
                    result.perimiter + current.Steps,
                    result.area + (next.X * current.Direction.Y * current.Steps)
                );
            },
            static result => result.area + (result.perimiter / 2) + 1
        );
}

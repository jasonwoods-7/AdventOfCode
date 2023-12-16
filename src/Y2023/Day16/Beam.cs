namespace AoC.Y2023.Day16;

record Beam(Coord Current, Coord Direction)
{
    public static readonly Coord North = new(0, -1);
    public static readonly Coord West = new(-1, 0);
    public static readonly Coord South = new(0, 1);
    public static readonly Coord East = new(1, 0);

    public Beam[] Advance(char instruction) => instruction switch
    {
        '.' => [this with { Current = Current + Direction }],
        '\\' => Direction switch
        {
            (0, -1) => [new Beam(Current + West, West)],
            (-1, 0) => [new Beam(Current + North, North)],
            (0, 1) => [new Beam(Current + East, East)],
            (1, 0) => [new Beam(Current + South, South)],
            _ => throw new InvalidOperationException()
        },
        '/' => Direction switch
        {
            (0, -1) => [new Beam(Current + East, East)],
            (-1, 0) => [new Beam(Current + South, South)],
            (0, 1) => [new Beam(Current + West, West)],
            (1, 0) => [new Beam(Current + North, North)],
            _ => throw new InvalidOperationException()
        },
        '-' => Direction switch
        {
            (0, -1) => [new Beam(Current + East, East), new Beam(Current + West, West)],
            (0, 1) => [new Beam(Current + East, East), new Beam(Current + West, West)],
            _ => [this with { Current = Current + Direction }]
        },
        '|' => Direction switch
        {
            (-1, 0) => [new Beam(Current + North, North), new Beam(Current + South, South)],
            (1, 0) => [new Beam(Current + North, North), new Beam(Current + South, South)],
            _ => [this with { Current = Current + Direction }]
        },
        _ => throw new InvalidOperationException()
    };
}

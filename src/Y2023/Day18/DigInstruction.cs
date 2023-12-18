namespace AoC.Y2023.Day18;

public record DigInstruction(Coord Direction, int Steps, int Color)
{
    public DigInstruction Swap()
    {
        var direction = (Color & 0xF) switch
        {
            0 => Coord.Right,
            1 => Coord.Down,
            2 => Coord.Left,
            3 => Coord.Up,
            _ => throw new InvalidOperationException()
        };

        var steps = Color >> 4;

        return new DigInstruction(direction, steps, (Steps << 4) | Direction switch
        {
            (1, 0) => 0,
            (0, 1) => 1,
            (-1, 0) => 2,
            (0, -1) => 3,
            _ => throw new InvalidOperationException()
        });
    }
}

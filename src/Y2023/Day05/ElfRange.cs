namespace AoC.Y2023.Day05;

[DebuggerDisplay("({Start} - {End})")]
public record ElfRange(long Start, long Length)
{
    public long End { get; } = Start + Length;

    public bool InRange(long x) => Start <= x && x < End;
}

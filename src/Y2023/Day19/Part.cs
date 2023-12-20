namespace AoC.Y2023.Day19;

public record Part
(
    [property: UsedImplicitly] int X,
    [property: UsedImplicitly] int M,
    [property: UsedImplicitly] int A,
    [property: UsedImplicitly] int S
)
{
    public int Total { get; } = X + M + A + S;
}

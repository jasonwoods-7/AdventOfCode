namespace AoC.Y2025.Day05;

public record ParsedInput(
    ImmutableList<(long lo, long hi)> Ranges,
    ImmutableList<long> Ingredients
);

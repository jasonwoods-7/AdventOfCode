namespace AoC.Y2023.Day19;

public record Parsed
(
    IReadOnlyDictionary<string, Instruction> Instructions,
    IReadOnlyList<Part> Parts
);

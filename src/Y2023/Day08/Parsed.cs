namespace AoC.Y2023.Day08;

public record Parsed(
    string Sequence,
    IReadOnlyDictionary<string, (string left, string right)> Maps
);

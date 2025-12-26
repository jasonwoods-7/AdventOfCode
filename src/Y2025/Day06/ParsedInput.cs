using CommunityToolkit.HighPerformance;

namespace AoC.Y2025.Day06;

public record ParsedInput(ReadOnlyMemory2D<char> Digits, ImmutableList<char> Operators);

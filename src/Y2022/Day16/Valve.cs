namespace AoC.Y2022.Day16;

public record Valve(string Name, int FlowRate, IReadOnlyList<string> ConnectedValves);

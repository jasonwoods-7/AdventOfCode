namespace AoC.Y2022.Day16;

sealed class DistanceFinder
{
    readonly IReadOnlyDictionary<string, Valve> _valves;
    readonly Func<(Valve, Valve), int> _findDistance;

    public DistanceFinder(IReadOnlyDictionary<string, Valve> valves)
    {
        _valves = valves;
        _findDistance = memo<(Valve, Valve), int>(InternalDistanceToValve);
    }

    public int DistanceToValve(Valve start, Valve end) => _findDistance((start, end));

    int InternalDistanceToValve((Valve start, Valve end) valves) => SuperEnumerable
        .GetShortestPathCost<Valve, int>(
            valves.start,
            (v, c) => v.ConnectedValves.Select(n => (_valves[n], c + 1)),
            valves.end);
}

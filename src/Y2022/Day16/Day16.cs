namespace AoC.Y2022.Day16;

public partial class Day16 : IAoCRunner<IReadOnlyDictionary<string, Valve>, int>
{
    [GeneratedRegex(@"^Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.*)$")]
    private static partial Regex ParseRegex();

    public IReadOnlyDictionary<string, Valve> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(static l =>
        {
            var match1 = ParseRegex().Match(l);
            Debug.Assert(match1.Success);

            var name = match1.Groups[1].Value;
            var flowRate = int.Parse(match1.Groups[2].Value, CultureInfo.CurrentCulture);
            var connected = match1.Groups[3].Value.Split(", ");

            return new Valve(name, flowRate, connected);
        })
        .ToDictionary(v => v.Name);

    public int RunPart1(IReadOnlyDictionary<string, Valve> input) => Solve(input, 30, true);

    public int RunPart2(IReadOnlyDictionary<string, Valve> input) => Solve(input, 26, false);

    static int Solve(
        IReadOnlyDictionary<string, Valve> valves,
        int timeRemaining,
        bool singleWorker)
    {
        var start = valves["AA"];

        var valvesToVisit = valves
            .Values
            .Where(v => v.FlowRate != 0)
            .OrderByDescending(v => v.FlowRate)
            .ToList();

        var unopenedValves = valvesToVisit
            .Select(v => v.Name)
            .ToImmutableHashSet();

        var distanceFinder = new DistanceFinder(valves);

        return FindMaxFlowRate(
            new Worker(start, 0),
            new Worker(start, singleWorker ? int.MaxValue : 0),
            valvesToVisit,
            unopenedValves,
            distanceFinder.DistanceToValve,
            0,
            0,
            timeRemaining);
    }

    static int FindMaxFlowRate(
        Worker worker1,
        Worker worker2,
        IReadOnlyList<Valve> valvesToVisit,
        ImmutableHashSet<string> unopenedValves,
        Func<Valve, Valve, int> distanceToValve,
        int currentFlowRate,
        int maxFlowRate,
        int timeRemaining)
    {
        Debug.Assert(worker1.Distance == 0 || worker2.Distance == 0);

        var nextWorkers = new IReadOnlyList<Worker>[]
        {
            System.Array.Empty<Worker>(),
            System.Array.Empty<Worker>()
        };

        for (var worker = 0; worker < 2; ++worker)
        {
            var currentWorker = worker == 0 ? worker1 : worker2;

            if (currentWorker.Distance > 0)
            {
                nextWorkers[worker] = new[] { currentWorker with { Distance = currentWorker.Distance - 1 } };
            }
            else if (unopenedValves.Contains(currentWorker.Valve.Name))
            {
                currentFlowRate += currentWorker.Valve.FlowRate * (timeRemaining - 1);

                if (currentFlowRate > maxFlowRate)
                {
                    maxFlowRate = currentFlowRate;
                }

                unopenedValves = unopenedValves.Remove(currentWorker.Valve.Name);

                Debug.Assert(currentWorker.Distance == 0);
                nextWorkers[worker] = new[] { currentWorker };
            }
            else
            {
                nextWorkers[worker] = valvesToVisit
                    .Where(v => unopenedValves.Contains(v.Name))
                    .Select(v => new Worker(v, distanceToValve(currentWorker.Valve, v) - 1))
                    .ToList();
            }
        }

        if (--timeRemaining < 1)
        {
            return maxFlowRate;
        }

        if (currentFlowRate + MaxPossibleRemaining(valvesToVisit, unopenedValves, timeRemaining) <= maxFlowRate)
        {
            return maxFlowRate;
        }

        foreach (var nextWorker1 in nextWorkers[0])
        {
            foreach (var nextWorker2 in nextWorkers[1])
            {
                if (nextWorker1.Valve == nextWorker2.Valve)
                {
                    continue;
                }

                var (worker1Next, worker2Next, distance) = FindNextPositions(nextWorker1, nextWorker2);

                maxFlowRate = FindMaxFlowRate(
                    worker1Next,
                    worker2Next,
                    valvesToVisit,
                    unopenedValves,
                    distanceToValve,
                    currentFlowRate,
                    maxFlowRate,
                    timeRemaining - distance);
            }
        }

        return maxFlowRate;
    }

    static int MaxPossibleRemaining(
        IReadOnlyList<Valve> valves,
        ImmutableHashSet<string> unopenedValves,
        int timeRemaining)
    {
        var flow = 0;

        foreach (var (i, valve) in valves.Where(v => unopenedValves.Contains(v.Name)).Index())
        {
            flow += valve.FlowRate;

            if ((i & 1) == 1)
            {
                if (--timeRemaining < 1)
                {
                    break;
                }
            }
        }

        flow += flow * timeRemaining;

        return flow;
    }

    static (Worker, Worker, int) FindNextPositions(Worker worker1, Worker worker2)
    {
        Debug.Assert(worker1.Distance >= 0 && worker2.Distance >= 0);

        if (worker1.Distance == 0 || worker2.Distance == 0)
        {
            return (worker1, worker2, 0);
        }

        var distance = Math.Min(worker1.Distance, worker2.Distance);

        return (
            worker1 with { Distance = worker1.Distance - distance },
            worker2 with { Distance = worker2.Distance - distance },
            distance);
    }
}

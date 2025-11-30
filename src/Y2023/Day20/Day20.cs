using AoC.Helpers;

namespace AoC.Y2023.Day20;

public class Day20 : IAoCRunner<IReadOnlyDictionary<string, Module>, long>
{
    public IReadOnlyDictionary<string, Module> ParseInput(IEnumerable<string> puzzleInput)
    {
        var modules = puzzleInput
            .Select<string, Module>(line =>
            {
                var destinationsStart = line.IndexOf("->", StringComparison.Ordinal);
                var name = line[..(destinationsStart - 1)];
                var destinations = line[(destinationsStart + 3)..].Split(", ").ToImmutableArray();

                return line[0] switch
                {
                    '%' => new Module.FlipFlop(name[1..], destinations),
                    '&' => new Module.Conjunction(name[1..], destinations),
                    _ => new Module.Broadcaster(name, destinations),
                };
            })
            .Append(new Module.Conjunction("rx", ImmutableArray<string>.Empty))
            .ToDictionary(m => m.Name);

        foreach (var module in modules)
        {
            module.Value.MatchConjunction(
                c =>
                {
                    foreach (
                        var current in modules.Where(m =>
                            m.Value.DestinationModules.Contains(c.Name)
                        )
                    )
                    {
                        c.AddInput(current.Value.Name);
                    }
                },
                () => { }
            );
        }

        return modules;
    }

    public long RunPart1(IReadOnlyDictionary<string, Module> input)
    {
        var untyped = new Dictionary<string, Module>();

        for (var i = 0; i < 1000; i++)
        {
            PressButton(input, untyped, i);
        }

        return input
            .Concat(untyped)
            .Aggregate(
                (low: 0L, high: 0L),
                static (result, current) =>
                    (result.low + current.Value.LowPulses, result.high + current.Value.HighPulses),
                result => result.low * result.high
            );
    }

    public long RunPart2(IReadOnlyDictionary<string, Module> input)
    {
        var inputs = input[input["rx"].UnwrapConjunction().GetInputs().Single()]
            .UnwrapConjunction()
            .GetInputs()
            .ToList();
        var cycles = new Dictionary<string, long>();

        var finished = false;

        foreach (var conj in inputs)
        {
            input[conj].UnwrapConjunction().AllHigh += (sender, args) =>
            {
                var name = ((Module)sender!).Name;
                if (!cycles.ContainsKey(name))
                {
                    cycles.Add(name, args.ButtonPresses);
                    finished = cycles.Count == inputs.Count;
                }
            };
        }

        var untyped = new Dictionary<string, Module>();
        var buttonPress = 0L;

        while (!finished)
        {
            PressButton(input, untyped, ++buttonPress);
        }

        return cycles.Values.Aggregate(NumberHelpers.LeastCommonMultiple);
    }

    static void PressButton(
        IReadOnlyDictionary<string, Module> input,
        Dictionary<string, Module> untyped,
        long buttonPress
    )
    {
        var queue = new Queue<(string, string, Pulse)>();
        queue.Enqueue(("", "broadcaster", Pulse.Low));

        while (queue.TryDequeue(out var result))
        {
            var (sender, receiver, pulse) = result;

            if (!input.TryGetValue(receiver, out var module))
            {
                if (!untyped.TryGetValue(receiver, out module))
                {
                    module = new Module.Untyped(receiver);
                    untyped.Add(receiver, module);
                }
            }

            foreach (var next in module.Send(sender, pulse, buttonPress))
            {
                queue.Enqueue(next);
            }
        }
    }
}

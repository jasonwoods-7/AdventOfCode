namespace AoC.Y2023.Day20;

[Union]
#pragma warning disable CA1716
public abstract partial record Module
#pragma warning restore CA1716
{
    public partial record Broadcaster(string Name, ImmutableArray<string> DestinationModules);

    public partial record FlipFlop(string Name, ImmutableArray<string> DestinationModules);

    public partial record Conjunction(string Name, ImmutableArray<string> DestinationModules);

    public partial record Untyped(string Name);

    public abstract string Name { get; init; }
    public abstract ImmutableArray<string> DestinationModules { get; init; }

    public long LowPulses { get; private set; }
    public long HighPulses { get; private set; }

    public IEnumerable<(string, string, Pulse)> Send(string sender, Pulse pulse, long buttonPress)
    {
        if (pulse == Pulse.Low)
        {
            LowPulses++;
        }
        else
        {
            HighPulses++;
        }

        return SendInternal(sender, pulse, buttonPress);
    }

    protected abstract IEnumerable<(string, string, Pulse)> SendInternal(string sender, Pulse pulse, long buttonPress);
}

namespace AoC.Y2023.Day05;

[DebuggerDisplay("{Source} => {Destination}")]
public record ElfMap(ElfRange Destination, ElfRange Source)
{
    public Option<long> GetDestination(long source) =>
        source switch
        {
            _ when Source.InRange(source) => Option<long>.Some(
                source - Source.Start + Destination.Start
            ),
            _ => Option<long>.None,
        };

    public Option<long> GetSource(long destination) =>
        destination switch
        {
            _ when Destination.InRange(destination) => Option<long>.Some(
                destination - Destination.Start + Source.Start
            ),
            _ => Option<long>.None,
        };
}

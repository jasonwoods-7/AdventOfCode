namespace AoC.Y2023.Day05;

[DebuggerDisplay("({Start} - {End})")]
public record ElfRange(long Start, long Length)
{
    public long End { get; } = Start + Length;

    public bool InRange(long x) => Start <= x && x < End;
}

[DebuggerDisplay("{Source} => {Destination}")]
public record ElfMap(ElfRange Destination, ElfRange Source)
{
    public Option<long> GetDestination(long source) => source switch
    {
        _ when Source.InRange(source) => Option<long>.Some(source - Source.Start + Destination.Start),
        _ => Option<long>.None
    };

    public Option<long> GetSource(long destination) => destination switch
    {
        _ when Destination.InRange(destination) => Option<long>.Some(destination - Destination.Start + Source.Start),
        _ => Option<long>.None
    };
}

public record ElfMaps(IReadOnlyList<ElfMap> Ranges)
{
    public static ElfMaps ParseMaps(IEnumerable<string> maps) => maps
        .Select(m => m
            .FindNumbers<long>()
            .Fold((ds, ss, l) => new ElfMap(new ElfRange(ds, l), new ElfRange(ss, l))))
        .Apply(ms => new ElfMaps(ms.OrderBy(m => m.Source.Start).ToList()));

    public long FindDestination(long source) => Ranges
        .Aggregate(
            Option<long>.None,
            (result, range) => result.BiBind(i => i, () => range.GetDestination(source)),
            result => result.Match(i => i, () => source));

    public long FindSource(long destination) => Ranges
        .Aggregate(
            Option<long>.None,
            (result, range) => result.BiBind(i => i, () => range.GetSource(destination)),
            result => result.Match(i => i, () => destination));
}

public record Parsed
(
    IReadOnlyList<long> Seeds,
    ElfMaps SeedToSoilMaps,
    ElfMaps SoilToFertilizerMaps,
    ElfMaps FertilizerToWaterMaps,
    ElfMaps WaterToLightMaps,
    ElfMaps LightToTemperatureMaps,
    ElfMaps TemperatureToHumidityMaps,
    ElfMaps HumidityToLocationMaps
)
{
    public long SeedToLocation(long seed) => seed
        .Apply(SeedToSoilMaps.FindDestination)
        .Apply(SoilToFertilizerMaps.FindDestination)
        .Apply(FertilizerToWaterMaps.FindDestination)
        .Apply(WaterToLightMaps.FindDestination)
        .Apply(LightToTemperatureMaps.FindDestination)
        .Apply(TemperatureToHumidityMaps.FindDestination)
        .Apply(HumidityToLocationMaps.FindDestination);

    public long LocationToSeed(long location) => location
        .Apply(HumidityToLocationMaps.FindSource)
        .Apply(TemperatureToHumidityMaps.FindSource)
        .Apply(LightToTemperatureMaps.FindSource)
        .Apply(WaterToLightMaps.FindSource)
        .Apply(FertilizerToWaterMaps.FindSource)
        .Apply(SoilToFertilizerMaps.FindSource)
        .Apply(SeedToSoilMaps.FindSource);
}

public class Day05 : IAoCRunner<Parsed, long>
{
    public Parsed ParseInput(IEnumerable<string> puzzleInput)
    {
        var input = puzzleInput
            .Split(l => l.Length == 0)
            .Map(l => l.ToList())
            .ToList();

        var seeds = input[0][0]
            .FindNumbers<long>()
            .ToList();

        return new Parsed(
            seeds,
            ElfMaps.ParseMaps(input[1].Skip(1)),
            ElfMaps.ParseMaps(input[2].Skip(1)),
            ElfMaps.ParseMaps(input[3].Skip(1)),
            ElfMaps.ParseMaps(input[4].Skip(1)),
            ElfMaps.ParseMaps(input[5].Skip(1)),
            ElfMaps.ParseMaps(input[6].Skip(1)),
            ElfMaps.ParseMaps(input[7].Skip(1))
        );
    }

    public long RunPart1(Parsed input) => input
        .Seeds
        .Min(input.SeedToLocation);

    public long RunPart2(Parsed input)
    {
        var ranges = input
            .Seeds
            .Batch(2)
            .Select(r => r.Fold((start, length) => new ElfRange(start, length)))
            .ToList();

        return SuperEnumerable
            .Generate(0L, l => ++l)
            .Select(input.LocationToSeed)
            .Where(s => ranges.Any(r => r.InRange(s)))
            .Select(input.SeedToLocation)
            .First();
    }
}

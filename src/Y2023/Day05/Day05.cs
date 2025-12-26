namespace AoC.Y2023.Day05;

public class Day05 : IAoCRunner<Parsed, long>
{
    public Parsed ParseInput(IEnumerable<string> puzzleInput)
    {
        var input = puzzleInput.Split(l => l.Length == 0).Map(l => l.ToList()).ToList();

        var seeds = input[0][0].FindNumbers<long>().ToList();

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

    public long RunPart1(
        Parsed input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Seeds.Min(input.SeedToLocation);

    public long RunPart2(
        Parsed input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        var ranges = input
            .Seeds.Batch(2)
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

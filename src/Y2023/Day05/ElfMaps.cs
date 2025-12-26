namespace AoC.Y2023.Day05;

public record ElfMaps(IReadOnlyList<ElfMap> Ranges)
{
    public static ElfMaps ParseMaps(IEnumerable<string> maps) =>
        maps.Select(m =>
                m.FindNumbers<long>()
                    .Fold((ds, ss, l) => new ElfMap(new ElfRange(ds, l), new ElfRange(ss, l)))
            )
            .Apply(ms => new ElfMaps(ms.OrderBy(m => m.Source.Start).ToList()));

    public long FindDestination(long source) =>
        Ranges.Aggregate(
            Option<long>.None,
            (result, range) => result.BiBind(i => i, () => range.GetDestination(source)),
            result => result.Match(i => i, () => source)
        );

    public long FindSource(long destination) =>
        Ranges.Aggregate(
            Option<long>.None,
            (result, range) => result.BiBind(i => i, () => range.GetSource(destination)),
            result => result.Match(i => i, () => destination)
        );
}

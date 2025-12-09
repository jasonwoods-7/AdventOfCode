namespace AoC.Y2025.Day05;

public record ParsedInput(
    ImmutableList<(long lo, long hi)> Ranges,
    ImmutableList<long> Ingredients
);

public class Day05 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(l => l.Length == 0)
            .Fold(
                (ranges, ingredients) =>
                    new ParsedInput(
                        ranges
                            .Select(r => r.FindNumbers<long>().Fold((lo, hi) => (lo, -hi)))
                            .ToImmutableList(),
                        ingredients.Select(i => i.ParseNumber<long>()).ToImmutableList()
                    )
            );

    public long RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => input.Ingredients.Count(i => input.Ranges.Any(r => i >= r.lo && i <= r.hi));

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Ranges.OrderBy(r => r.lo)
            .ThenBy(r => r.hi)
            .Aggregate(
                ImmutableList<(long lo, long hi)>.Empty,
                (accumulator, current) =>
                {
                    for (var counter = 0; counter < accumulator.Count; ++counter)
                    {
                        if (
                            accumulator[counter].lo <= current.lo
                            && accumulator[counter].hi >= current.hi
                        )
                        {
                            return accumulator;
                        }

                        if (
                            accumulator[counter].hi >= current.lo
                            && current.hi >= accumulator[counter].hi
                        )
                        {
                            return accumulator.SetItem(
                                counter,
                                (accumulator[counter].lo, current.hi)
                            );
                        }

                        if (
                            accumulator[counter].lo <= current.hi
                            && current.lo <= accumulator[counter].lo
                        )
                        {
                            return accumulator.SetItem(
                                counter,
                                (current.lo, accumulator[counter].hi)
                            );
                        }
                    }

                    return accumulator.Add(current);
                },
                accumulator => accumulator.Sum(r => r.hi - r.lo + 1)
            );
}

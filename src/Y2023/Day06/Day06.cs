namespace AoC.Y2023.Day06;

public record Race(long Time, long Distance);

public class Day06 : IAoCRunner<IReadOnlyList<Race>, long>
{
    public IReadOnlyList<Race> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Fold(
            (times, distances) =>
                times
                    .FindNumbers<long>()
                    .Zip(distances.FindNumbers<long>(), (t, d) => new Race(t, d))
                    .ToList()
        );

    public long RunPart1(
        IReadOnlyList<Race> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Select(r => Range(r.Time - 1).Count(i => i * (r.Time - i) > r.Distance)).Product();

    public long RunPart2(
        IReadOnlyList<Race> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Aggregate(
                (time: "", distance: ""),
                (result, race) => (result.time + race.Time, result.distance + race.Distance),
                result => new Race(
                    result.time.ParseNumber<long>(),
                    result.distance.ParseNumber<long>()
                )
            )
            .Apply(r => Range(r.Time - 1).AsParallel().Count(i => i * (r.Time - i) > r.Distance));

    static IEnumerable<long> Range(long end)
    {
        var l = 1L;

        while (l < end)
        {
            yield return l++;
        }
    }
}

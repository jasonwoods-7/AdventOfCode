namespace AoC.Y2023.Day06;

public record Race(long Time, long Distance);

public class Day06 : IAoCRunner<IReadOnlyList<Race>, long>
{
    public IReadOnlyList<Race> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Fold((times, distances) => times
            .FindNumbers<long>()
            .Zip(
                distances.FindNumbers<long>(),
                (t, d) => new Race(t, d))
            .ToList());

    public long RunPart1(IReadOnlyList<Race> input) => input
        .Select(r => Range(r.Time - 1)
            .Count(i => i * (r.Time - i) > r.Distance))
        .Product();

    public long RunPart2(IReadOnlyList<Race> input) => input
        .Aggregate(
            (time: "", distance: ""),
            (result, race) => (result.time + race.Time, result.distance + race.Distance),
            result => new Race(long.Parse(result.time, CultureInfo.CurrentCulture), long.Parse(result.distance, CultureInfo.CurrentCulture)))
        .Apply(r => Range(r.Time - 1)
            .AsParallel()
            .Count(i => i * (r.Time - i) > r.Distance));

    static IEnumerable<long> Range(long end)
    {
        var l = 1L;

        while (l < end)
        {
            yield return l++;
        }
    }
}

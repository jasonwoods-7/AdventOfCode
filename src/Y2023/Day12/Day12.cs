using System.Numerics;

namespace AoC.Y2023.Day12;

public enum SpringStatus
{
    Unknown = -1,
    Operational,
    Damaged
}

public record Row(IReadOnlyList<SpringStatus> Springs, IReadOnlyList<int> Groups)
{
    public int PossibleArrangements()
    {
        var (damaged, unknown) = Springs
            .Reverse()
            .Index()
            .Aggregate((d: 0UL, u: ImmutableList<int>.Empty), (result, current) => current.item switch
            {
                SpringStatus.Unknown => (result.d, result.u.Add(current.index)),
                SpringStatus.Damaged => (result.d | (1UL << current.index), result.u),
                _ => result
            });

        var total = Groups.Sum();
        var on = total - BitOperations.PopCount(damaged);

        if (on == 0)
        {
            return 1;
        }

        var reversedGroups = Groups.Reverse().ToList();

        return (
            from c in unknown
            select 1UL << c)
            .Subsets(on)
            .Select(s => s.Aggregate((r, c) => r | c))
            .Select(check => damaged | check)
            .Count(current => SequentialBitCounts(current)
                .CollectionsEqual(reversedGroups));
    }

    static IEnumerable<int> SequentialBitCounts(ulong value)
    {
        var currentCount = 0;

        do
        {
            var b = value & 1;
            if (b == 0)
            {
                if (currentCount != 0)
                {
                    yield return currentCount;
                    currentCount = 0;
                }
                continue;
            }

            currentCount++;
        } while ((value >>= 1) > 0);

        if (currentCount != 0)
        {
            yield return currentCount;
        }
    }
}

public class Day12(bool part2) : IAoCRunner<IReadOnlyList<Row>, int>
{
    public IReadOnlyList<Row> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(line => line
            .Split(' ')
            .Fold((springs, groups) => new Row(
                Enumerable
                    .Repeat(springs, part2 ? 5 : 1)
                    .Apply(s => string.Join("?", s))
                    .Select(s => s switch
                    {
                        '?' => SpringStatus.Unknown,
                        '.' => SpringStatus.Operational,
                        '#' => SpringStatus.Damaged,
                        _ => throw new InvalidOperationException()
                    })
                    .ToList(),
                Enumerable
                    .Repeat(groups, part2 ? 5 : 1)
                    .Apply(s => string.Join(",", s))
                    .FindNumbers<int>()
                    .ToList())))
        .ToList();

    public int RunPart1(IReadOnlyList<Row> input) => input
        .Sum(r => r.PossibleArrangements());

    public int RunPart2(IReadOnlyList<Row> input) => input
        .Sum(r => r.PossibleArrangements());
}

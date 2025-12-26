using ParsedInput = System.Collections.Immutable.ImmutableList<System.Collections.Immutable.ImmutableArray<long>>;

namespace AoC.Y2025.Day12;

public class Day12 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(l => l.Length == 0)
            .Last()
            .Select(l => l.FindNumbers<long>().ToImmutableArray())
            .ToImmutableList();

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => input.Count(nums => nums[0] * nums[1] >= 9L * nums[2..].Sum());

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) => -1L;
}

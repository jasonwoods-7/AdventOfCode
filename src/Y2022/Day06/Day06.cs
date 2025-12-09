namespace AoC.Y2022.Day06;

public class Day06 : IAoCRunner<string, int>
{
    public string ParseInput(IEnumerable<string> puzzleInput) => puzzleInput.First();

    public int RunPart1(
        string input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => FindStartOfMessageMarker(input, 4);

    public int RunPart2(
        string input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => FindStartOfMessageMarker(input, 14);

    static int FindStartOfMessageMarker(string input, int windowSize) =>
        input
            .Window(windowSize)
            .Index()
            .Choose(p => (p.Item.ToHashSet().Count == windowSize, p.Index + windowSize))
            .First();
}

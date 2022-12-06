using static MoreLinq.Extensions.ChooseExtension;
using static MoreLinq.Extensions.IndexExtension;
using static MoreLinq.Extensions.WindowExtension;

namespace AoC.Y2022.Day06;

public class Day06 : IAoCRunner<string, int>
{
    public string ParseInput(IEnumerable<string> puzzleInput) => puzzleInput.First();

    public int RunPart1(string input) => FindStartOfMessageMarker(input, 4);

    public int RunPart2(string input) => FindStartOfMessageMarker(input, 14);

    static int FindStartOfMessageMarker(string input, int windowSize) => input
        .Window(windowSize)
        .Index()
        .Choose(p => (p.Value.ToHashSet().Count == windowSize, p.Key + windowSize))
        .First();
}

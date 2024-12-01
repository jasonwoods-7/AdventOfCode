using AoC.Helpers;

namespace AoC.Y2023.Day08;

public record Parsed(string Sequence, IReadOnlyDictionary<string, (string left, string right)> Maps);

public class Day08 : IAoCRunner<Parsed, long>
{
    public Parsed ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Split(l => l.Length == 0)
        .Fold((rawSequence, rawMaps) =>
        {
            var sequence = rawSequence[0];

            var maps = rawMaps
                .Select(m => (m[..3], m[7..10], m[12..15]))
                .ToDictionary(t => t.Item1, t => (t.Item2, t.Item3));

            return new Parsed(sequence, maps);
        });

    public long RunPart1(Parsed input) =>
        FindEnd(input, "AAA", e => e == "ZZZ", 0);

    public long RunPart2(Parsed input) => input
        .Maps
        .Keys
        .Where(k => k[2] == 'A')
        .Select(n => FindEnd(input, n, e => e[2] == 'Z', 0))
        .Aggregate(NumberHelpers.LeastCommonMultiple);

    static long FindEnd(Parsed input, string currentNode, Func<string, bool> atEnd, long count)
    {
        while (!atEnd(currentNode))
        {
            currentNode = input.Sequence[(int)count++ % input.Sequence.Length] == 'L'
                ? input.Maps[currentNode].left
                : input.Maps[currentNode].right;
        }

        return count;
    }
}

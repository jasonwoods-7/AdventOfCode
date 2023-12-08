namespace AoC.Y2023.Day07;

public class Day07 : IAoCRunner<IEnumerable<string>, int>
{
    public IEnumerable<string> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput;

    public int RunPart1(IEnumerable<string> input) => Solve(input, Hand.Part1Scorer, Card.Jack);

    public int RunPart2(IEnumerable<string> input) => Solve(input, Hand.Part2Scorer, Card.Joker);

    static int Solve(IEnumerable<string> input, Func<Card[], HandKind> scorer, Card jCard) => input
        .Select(l => Hand.ParseHand(l, scorer, jCard))
        .Order()
        .Index(1)
        .Sum(t => t.index * t.item.Bet);
}

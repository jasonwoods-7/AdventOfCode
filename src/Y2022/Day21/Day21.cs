namespace AoC.Y2022.Day21;

public class Day21 : IAoCRunner<Tree, long>
{
    public Tree ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.ToDictionary(l => l[..4], l => l[6..]).Apply(t => ParseTree("root", t));

    public long RunPart1(Tree input) => input.CalculateValue();

    public long RunPart2(Tree input)
    {
        var root = input as Branch;
        Debug.Assert(root is not null);

        var (humanCalc, monkeyCalc, human) = (
            root.Left.Find("humn"),
            root.Right.Find("humn")
        ) switch
        {
            (null, var h) => (root.Right, root.Left, h),
            (var h, null) => (root.Left, root.Right, h),
            _ => throw new InvalidOperationException(),
        };

        var monkeyValue = monkeyCalc.CalculateValue();

        return humanCalc.FindHumanValue(monkeyValue, human!);
    }

    static Tree ParseTree(string name, IReadOnlyDictionary<string, string> allTrees)
    {
        var value = allTrees[name];

        if (long.TryParse(value, out var result))
        {
            return new Leaf(name, result);
        }

        var left = ParseTree(value[..4], allTrees);
        var right = ParseTree(value[7..], allTrees);
        var binaryOperator = value[5];

        return new Branch(name, left, right, binaryOperator);
    }
}

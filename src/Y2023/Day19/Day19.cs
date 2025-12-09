using System.Linq.Expressions;

namespace AoC.Y2023.Day19;

public class Day19 : IAoCRunner<Parsed, long>
{
    public Parsed ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(l => l.Length == 0)
            .Fold(
                (rawInstructions, rawParts) =>
                {
                    var instructions = rawInstructions
                        .Select(i =>
                        {
                            var instructionStart = i.IndexOf('{');
                            var name = i[..instructionStart];
                            var instruction = InstructionParser.ParseInstruction(
                                i[(instructionStart + 1)..^1]
                            );
                            return (name, instruction);
                        })
                        .ToDictionary();

                    var parts = rawParts
                        .Select(p =>
                            p.FindNumbers<int>().Fold((x, m, a, s) => new Part(x, m, a, s))
                        )
                        .ToList();

                    return new Parsed(instructions, parts);
                }
            );

    public long RunPart1(
        Parsed input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        input
            .Parts.Choose(p =>
            {
                var instruction = input.Instructions["in"];

                while (instruction.MatchWorkflow(w => w.Next is not ("A" or "R"), () => true))
                {
                    instruction = instruction.Match(
                        r => r.Predicate(p) ? r.True : r.False,
                        w => input.Instructions[w.Next]
                    );
                }

                return (instruction.MatchWorkflow(w => w.Next == "A", () => false), p.Total);
            })
            .Sum();

    public long RunPart2(
        Parsed input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var hyperCube = new Range(1, 4000).Apply(r => new HyperCube(r, r, r, r));

        return CalculateTotal(hyperCube, input.Instructions, input.Instructions["in"]);
    }

    static long CalculateTotal(
        HyperCube cube,
        IReadOnlyDictionary<string, Instruction> instructions,
        Instruction instruction
    ) =>
        instruction.Match(
            r => HandleRule(r, cube, instructions),
            w =>
                w.Next switch
                {
                    "A" => cube.CalculateArea(),
                    "R" => 0L,
                    _ => CalculateTotal(cube, instructions, instructions[w.Next]),
                }
        );

    static long HandleRule(
        Instruction.Rule rule,
        HyperCube cube,
        IReadOnlyDictionary<string, Instruction> instructions
    )
    {
        var binary = (BinaryExpression)rule.Expression.Body;
        var name = ((MemberExpression)binary.Left).Member.Name;
        var value = (int)((ConstantExpression)binary.Right).Value!;

        if (binary.NodeType == ExpressionType.LessThan)
        {
            var (low, high) = cube.SplitCube(value - 1, name);

            return CalculateTotal(low, instructions, rule.True)
                + CalculateTotal(high, instructions, rule.False);
        }

        {
            var (low, high) = cube.SplitCube(value, name);

            return CalculateTotal(low, instructions, rule.False)
                + CalculateTotal(high, instructions, rule.True);
        }
    }
}

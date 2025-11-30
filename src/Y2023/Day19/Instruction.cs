using System.Linq.Expressions;

namespace AoC.Y2023.Day19;

[Union]
public partial record Instruction
{
    public partial record Rule(
        Expression<Func<Part, bool>> Expression,
        Func<Part, bool> Predicate,
        Instruction True,
        Instruction False
    );

    public partial record Workflow(string Next);
}

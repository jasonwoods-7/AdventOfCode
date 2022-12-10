using AnyOfTypes;

namespace AoC.Y2022.Day10;

public class Day10 : IAoCRunner<IEnumerable<IInstruction>, AnyOf<int, string>>
{
    public IEnumerable<IInstruction> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(InstructionFactory.CreateInstruction);

    public AnyOf<int, string> RunPart1(IEnumerable<IInstruction> input) => input.Aggregate(
        new SystemState(),
        (state, instruction) => instruction.Accept(state),
        state => state.InterestingSum);

    public AnyOf<int, string> RunPart2(IEnumerable<IInstruction> input) => input.Aggregate(
        new SystemState(),
        (state, instruction) => instruction.Accept(state),
        state => state.RenderConsole());
}

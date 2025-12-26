namespace AoC.Y2022.Day10;

public class Day10 : IAoCRunner<IEnumerable<IInstruction>, AnyOf<int, string>>
{
    public IEnumerable<IInstruction> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(InstructionFactory.CreateInstruction);

    public AnyOf<int, string> RunPart1(
        IEnumerable<IInstruction> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Aggregate(
            new SystemState<InterestingSumCollectionStrategy>(),
            static (state, instruction) => instruction.Accept(state),
            static state => state.CollectionStrategy.InterestingSum
        );

    public AnyOf<int, string> RunPart2(
        IEnumerable<IInstruction> input,
        object? state = null,
        CancellationToken cancellationToken = default
    ) =>
        input.Aggregate(
            new SystemState<ConsoleRenderingCollectionStrategy>(),
            static (state, instruction) => instruction.Accept(state),
            static state => state.CollectionStrategy.RenderConsole()
        );
}

namespace AoC.Y2022.Day10;

public class CycleInstructionDecorator : IInstruction
{
    readonly IInstruction _childInstruction;

    public CycleInstructionDecorator(IInstruction childInstruction) =>
        _childInstruction = childInstruction;

    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> =>
        _childInstruction.Accept(visitor.VisitCycle(this));
}

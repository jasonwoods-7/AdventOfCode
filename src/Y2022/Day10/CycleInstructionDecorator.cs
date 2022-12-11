namespace AoC.Y2022.Day10;

public class CycleInstructionDecorator : IInstruction
{
    public IInstruction ChildInstruction { get; }

    public CycleInstructionDecorator(IInstruction childInstruction) =>
        ChildInstruction = childInstruction;

    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> =>
        visitor.VisitCycle(this);
}

namespace AoC.Y2022.Day10;

public class ProcessingInstruction : IInstruction
{
    public IInstruction ChildInstruction { get; }

    public ProcessingInstruction(IInstruction childInstruction) =>
        ChildInstruction = childInstruction;

    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> =>
        visitor.VisitProcessing(this);
}

namespace AoC.Y2022.Day10;

public class NoopInstruction : IInstruction
{
    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> => visitor.VisitNoop();
}

namespace AoC.Y2022.Day10;

public class AddInstruction : IInstruction
{
    public int AddValue { get; }

    public AddInstruction(int value) => AddValue = value;

    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> => visitor.VisitAdd(this);
}

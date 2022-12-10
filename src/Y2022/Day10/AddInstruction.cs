namespace AoC.Y2022.Day10;

public class AddInstruction : IInstruction
{
    public AddInstruction(int value) =>
        AddValue = value;

    public int AddValue { get; }

    public T Accept<T>(T visitor)
        where T : IInstructionVisitor<T> =>
        visitor.VisitAdd(this);
}

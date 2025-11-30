namespace AoC.Y2022.Day10;

public interface IInstruction
{
    T Accept<T>(T visitor)
        where T : IInstructionVisitor<T>;
}

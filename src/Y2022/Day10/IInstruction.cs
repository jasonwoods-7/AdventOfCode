namespace AoC.Y2022.Day10;

public interface IInstruction
{
    int CyclesRemaining { get; }

    IInstruction AdvanceInstruction();

    int MutateState(int oldState);
}

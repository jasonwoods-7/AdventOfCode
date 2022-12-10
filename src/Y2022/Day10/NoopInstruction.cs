namespace AoC.Y2022.Day10;

class NoopInstruction : IInstruction
{
    public NoopInstruction(int cyclesRemaining = 1) =>
        CyclesRemaining = cyclesRemaining;

    public int CyclesRemaining { get; }

    public IInstruction AdvanceInstruction() =>
        new NoopInstruction(CyclesRemaining - 1);

    public int MutateState(int oldState) => oldState;
}

namespace AoC.Y2022.Day10;

class AddInstruction : IInstruction
{
    readonly int _addValue;

    public AddInstruction(int value, int cyclesRemaining = 2)
    {
        _addValue = value;
        CyclesRemaining = cyclesRemaining;
    }

    public int CyclesRemaining { get; }

    public IInstruction AdvanceInstruction() =>
        new AddInstruction(_addValue, CyclesRemaining - 1);

    public int MutateState(int oldState) => oldState + _addValue;
}

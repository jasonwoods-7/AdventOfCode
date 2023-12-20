namespace AoC.Y2023.Day19;

class ValueState(InstructionState instructionState) : IState
{
    public IState OnReadToken(string name)
    {
        var value = name.ParseNumber<int>();
        instructionState.SetValue(value);
        return instructionState;
    }

    public IState OnInequalityToken(char inequality) => throw new InvalidOperationException();

    public IState OnTrueToken() => throw new InvalidOperationException();

    public IState OnFalseToken() => throw new InvalidOperationException();

    public IState OnEndToken() => throw new InvalidOperationException();

    public Instruction ToInstruction() => throw new InvalidOperationException();
}

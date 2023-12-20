namespace AoC.Y2023.Day19;

class InstructionNameToken(string name) : IToken
{
    public IState Process(IState currentState) => currentState.OnReadToken(name);
}

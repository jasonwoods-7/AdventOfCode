namespace AoC.Y2023.Day19;

class NullToken : IToken
{
    public IState Process(IState currentState) => currentState;
}

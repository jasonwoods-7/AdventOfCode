namespace AoC.Y2023.Day19;

class EndToken : IToken
{
    public IState Process(IState currentState) => currentState.OnEndToken();
}

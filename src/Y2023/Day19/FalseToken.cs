namespace AoC.Y2023.Day19;

class FalseToken : IToken
{
    public IState Process(IState currentState) => currentState.OnFalseToken();
}

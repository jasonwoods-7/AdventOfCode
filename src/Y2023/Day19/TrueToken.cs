namespace AoC.Y2023.Day19;

class TrueToken : IToken
{
    public IState Process(IState currentState) => currentState.OnTrueToken();
}

namespace AoC.Y2023.Day19;

sealed class TrueToken : IToken
{
    public IState Process(IState currentState) => currentState.OnTrueToken();
}

namespace AoC.Y2023.Day19;

sealed class NullToken : IToken
{
    public IState Process(IState currentState) => currentState;
}

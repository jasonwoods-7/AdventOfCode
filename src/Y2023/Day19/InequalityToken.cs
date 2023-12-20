namespace AoC.Y2023.Day19;

class InequalityToken(char current) : IToken
{
    public IState Process(IState currentState) => currentState.OnInequalityToken(current);
}

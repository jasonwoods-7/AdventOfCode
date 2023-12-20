namespace AoC.Y2023.Day19;

interface IToken
{
    IState Process(IState currentState);
}

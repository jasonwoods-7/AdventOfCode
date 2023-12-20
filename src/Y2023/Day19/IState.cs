namespace AoC.Y2023.Day19;

interface IState
{
    IState OnReadToken(string name);
    IState OnInequalityToken(char inequality);
    IState OnTrueToken();
    IState OnFalseToken();
    IState OnEndToken();
    Instruction ToInstruction();
}

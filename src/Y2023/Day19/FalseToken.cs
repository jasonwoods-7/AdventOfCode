﻿namespace AoC.Y2023.Day19;

sealed class FalseToken : IToken
{
    public IState Process(IState currentState) => currentState.OnFalseToken();
}

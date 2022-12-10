﻿namespace AoC.Y2022.Day10;

public class AddInstruction : IInstruction
{
    public AddInstruction(int value, int cyclesRemaining = 2)
    {
        AddValue = value;
        CyclesRemaining = cyclesRemaining;
    }

    public int AddValue { get; }
    public int CyclesRemaining { get; }

    public T Accept<T>(T state)
        where T : ISystemStateVisitor<T> =>
        state.VisitAdd(this);
}

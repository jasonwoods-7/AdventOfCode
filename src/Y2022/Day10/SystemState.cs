﻿using System.Collections;
using System.Text;

namespace AoC.Y2022.Day10;

sealed class SystemState : IInstructionVisitor<SystemState>
{
    int _currentCycle;
    int _xRegister;
    readonly BitArray _console;

    public SystemState()
    {
        _currentCycle = 1;
        _xRegister = 1;
        _console = new BitArray(240, false);
        InterestingSum = 0;
    }

    public SystemState VisitNoop() => this;

    public SystemState VisitAdd(AddInstruction addInstruction)
    {
        _xRegister += addInstruction.AddValue;
        return this;
    }

    public SystemState VisitCycle(CycleInstruction cycleInstruction)
    {
        if (IsInterestingCycle(_currentCycle))
        {
            InterestingSum += _currentCycle * _xRegister;
        }

        _console[_currentCycle - 1] = ((_currentCycle - 1) % 40) switch
        {
            var i when _xRegister - 1 == i || _xRegister + 0 == i || _xRegister + 1 == i => true,
            _ => false
        };

        ++_currentCycle;

        return cycleInstruction.ChildInstruction.Accept(this);
    }

    public int InterestingSum { get; private set; }

    public string RenderConsole()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < 240; ++i)
        {
            builder.Append(_console[i] ? "#" : " ");
            if ((i + 1) % 40 == 0)
            {
                builder.AppendLine();
            }
        }

        return builder.ToString();
    }

    static bool IsInterestingCycle(int currentCycle) => currentCycle switch
    {
        20 or 60 or 100 or 140 or 180 or 220 => true,
        _ => false
    };
}
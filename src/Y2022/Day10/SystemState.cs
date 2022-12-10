using System.Text;

namespace AoC.Y2022.Day10;

class SystemState
{
    int _currentCycle;
    int _xRegister;
    readonly StringBuilder _console;

    public SystemState()
    {
        _currentCycle = 1;
        _xRegister = 1;
        _console = new StringBuilder();
        InterestingSum = 0;
    }

    public SystemState RunInstruction(IInstruction instruction)
    {
        var processedInstruction = instruction.AdvanceInstruction();

        if (IsInterestingCycle(_currentCycle))
        {
            InterestingSum += _currentCycle * _xRegister;
        }

        _console.Append(((_currentCycle - 1) % 40) switch
        {
            var i when _xRegister - 1 == i || _xRegister + 0 == i || _xRegister + 1 == i => "#",
            _ => "."
        });

        ++_currentCycle;

        if (processedInstruction.CyclesRemaining == 0)
        {
            _xRegister = processedInstruction.MutateState(_xRegister);
            return this;
        }

        return RunInstruction(processedInstruction);
    }

    public int InterestingSum { get; private set; }

    public string RenderConsole()
    {
        var builder = new StringBuilder(_console.ToString());

        for (var i = 240; i > 0; i -= 40)
        {
            builder.Insert(i, Environment.NewLine);
        }

        return builder.ToString();
    }

    static bool IsInterestingCycle(int currentCycle) => currentCycle switch
    {
        20 or 60 or 100 or 140 or 180 or 220 => true,
        _ => false
    };
}

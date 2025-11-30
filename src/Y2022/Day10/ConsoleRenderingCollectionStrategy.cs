using System.Collections;
using System.Text;

namespace AoC.Y2022.Day10;

sealed class ConsoleRenderingCollectionStrategy : ICollectionStrategy
{
    readonly BitArray _console;

    public ConsoleRenderingCollectionStrategy() => _console = new BitArray(240, false);

    public void Collect(int currentCycle, int xRegister) =>
        _console[currentCycle - 1] = ((currentCycle - 1) % 40) switch
        {
            var i when xRegister - 1 == i || xRegister + 0 == i || xRegister + 1 == i => true,
            _ => false,
        };

    public string RenderConsole()
    {
        var builder = new StringBuilder(240);

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
}

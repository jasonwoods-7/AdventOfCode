namespace AoC.Y2023.Day20;

public class ConjunctionEventArgs(long buttonPresses) : EventArgs
{
    public long ButtonPresses { get; } = buttonPresses;
}

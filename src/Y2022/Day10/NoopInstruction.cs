namespace AoC.Y2022.Day10;

public class NoopInstruction : IInstruction
{
    public T Accept<T>(T state)
        where T : ISystemStateVisitor<T> =>
        state.VisitNoop();
}

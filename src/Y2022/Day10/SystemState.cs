namespace AoC.Y2022.Day10;

sealed class SystemState<T> : IInstructionVisitor<SystemState<T>>
    where T : ICollectionStrategy, new()
{
    int _currentCycle;
    int _xRegister;

    public SystemState()
    {
        CollectionStrategy = new T();
        _currentCycle = 1;
        _xRegister = 1;
    }

    public T CollectionStrategy { get; }

    public SystemState<T> VisitNoop() => this;

    public SystemState<T> VisitAdd(AddInstruction addInstruction)
    {
        _xRegister += addInstruction.AddValue;
        return this;
    }

    public SystemState<T> VisitCycle(CycleInstructionDecorator cycleInstructionDecorator)
    {
        CollectionStrategy.Collect(_currentCycle++, _xRegister);
        return cycleInstructionDecorator.ChildInstruction.Accept(this);
    }
}

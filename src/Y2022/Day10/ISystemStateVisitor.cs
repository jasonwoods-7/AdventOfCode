namespace AoC.Y2022.Day10;

public interface ISystemStateVisitor<out TSelf>
    where TSelf : ISystemStateVisitor<TSelf>
{
    TSelf VisitNoop();
    TSelf VisitAdd(AddInstruction addInstruction);
}

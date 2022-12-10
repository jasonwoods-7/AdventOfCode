namespace AoC.Y2022.Day10;

public interface IInstructionVisitor<out TSelf>
    where TSelf : IInstructionVisitor<TSelf>
{
    TSelf VisitNoop();
    TSelf VisitAdd(AddInstruction addInstruction);
    TSelf VisitCycle(CycleInstruction cycleInstruction);
}

namespace AoC.Y2022.Day10;

static class InstructionFactory
{
    public static IInstruction CreateInstruction(string instruction)
    {
        if (instruction == "noop")
        {
            return CreateCycle(1, new NoopInstruction());
        }

        Debug.Assert(instruction.StartsWith("addx ", StringComparison.Ordinal));
        var value = int.Parse(instruction[5..], CultureInfo.CurrentCulture);
        return CreateCycle(2, new AddInstruction(value));
    }

    static IInstruction CreateCycle(int cyclesRemaining, IInstruction accumulate) =>
        cyclesRemaining switch
        {
            0 => accumulate,
            _ => CreateCycle(cyclesRemaining - 1, new CycleInstructionDecorator(accumulate))
        };
}

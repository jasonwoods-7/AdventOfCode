namespace AoC.Y2022.Day10;

static class InstructionFactory
{
    public static IInstruction CreateInstruction(string instruction)
    {
        if (instruction == "noop")
        {
            return new NoopInstruction();
        }

        var value = int.Parse(instruction[5..], CultureInfo.CurrentCulture);
        return new AddInstruction(value);
    }
}

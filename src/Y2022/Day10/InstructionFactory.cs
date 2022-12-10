﻿namespace AoC.Y2022.Day10;

static class InstructionFactory
{
    public static IInstruction CreateInstruction(string instruction)
    {
        if (instruction == "noop")
        {
            return new NoopInstruction();
        }

        Debug.Assert(instruction.StartsWith("addx ", StringComparison.Ordinal));
        var value = int.Parse(instruction[5..], CultureInfo.CurrentCulture);
        return new ProcessingInstruction(new AddInstruction(value));
    }
}

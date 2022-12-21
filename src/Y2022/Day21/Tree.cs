namespace AoC.Y2022.Day21;

public abstract class Tree
{
    protected Tree(string name) => Name = name;

    public string Name { get; }

    public abstract long CalculateValue();

    public abstract Tree? Find(string name);

    public abstract string DebuggerDisplay();

    public abstract long FindHumanValue(long value, Tree human);
}

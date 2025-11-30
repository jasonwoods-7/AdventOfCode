namespace AoC.Y2022.Day21;

[DebuggerDisplay("{DebuggerDisplay(),nq}")]
sealed class Leaf : Tree
{
    readonly long _value;

    public Leaf(string name, long value)
        : base(name) => _value = value;

    /// <inheritdoc />
    public override long CalculateValue() => _value;

    /// <inheritdoc />
    public override Tree? Find(string name) => Name == name ? this : null;

    public override string DebuggerDisplay() => $"{_value}";

    /// <inheritdoc />
    public override long FindHumanValue(long value, Tree human) => value;
}

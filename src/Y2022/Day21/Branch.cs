namespace AoC.Y2022.Day21;

[DebuggerDisplay("{DebuggerDisplay(),nq}")]
sealed class Branch : Tree
{
    public Tree Left { get; }
    public Tree Right { get; }

    readonly char _binaryOperator;

    public Branch(string name, Tree left, Tree right, char binaryOperator)
        : base(name)
    {
        Left = left;
        Right = right;
        _binaryOperator = binaryOperator;
    }

    /// <inheritdoc />
    public override long CalculateValue() =>
        _binaryOperator switch
        {
            '+' => Left.CalculateValue() + Right.CalculateValue(),
            '-' => Left.CalculateValue() - Right.CalculateValue(),
            '*' => Left.CalculateValue() * Right.CalculateValue(),
            '/' => Divide(Left.CalculateValue(), Right.CalculateValue()),
            _ => throw new InvalidOperationException(),
        };

    /// <inheritdoc />
    public override Tree? Find(string name) =>
        Name == name ? this : Left.Find(name) ?? Right.Find(name);

    public override string DebuggerDisplay() =>
        $"({Left.DebuggerDisplay()} {_binaryOperator} {Right.DebuggerDisplay()})";

    /// <inheritdoc />
    public override long FindHumanValue(long value, Tree human)
    {
        if (Left == human)
        {
            return Inverse(value, Right.CalculateValue(), true);
        }

        if (Right == human)
        {
            return Inverse(value, Left.CalculateValue(), false);
        }

        return (Left.Find(human.Name), Right.Find(human.Name)) switch
        {
            ({ }, null) => Left.FindHumanValue(Inverse(value, Right.CalculateValue(), true), human),
            (null, { }) => Right.FindHumanValue(
                Inverse(value, Left.CalculateValue(), false),
                human
            ),
            _ => throw new InvalidOperationException(),
        };
    }

    long Inverse(long left, long right, bool isLeft) =>
        (_binaryOperator, isLeft) switch
        {
            ('+', _) => left - right,
            ('-', true) => left + right,
            ('-', false) => right - left,
            ('*', _) => Divide(left, right),
            ('/', true) => left * right,
            ('/', false) => Divide(right, left),
            _ => throw new InvalidOperationException(),
        };

    static long Divide(long left, long right)
    {
        var (result, rem) = Math.DivRem(left, right);
        Debug.Assert(rem == 0, $"Expected rem to be 0, but found {rem}.");
        return result;
    }
}

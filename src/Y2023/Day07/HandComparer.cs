namespace AoC.Y2023.Day07;

partial record Hand
    : IComparable<Hand>
{
    /// <inheritdoc />
    public int CompareTo(Hand? other)
    {
        Debug.Assert(other is not null);

        var kindResult = Kind.CompareTo(other.Kind);

        if (kindResult != 0)
        {
            return kindResult;
        }

        for (var currentCard = 0; currentCard < 5; currentCard++)
        {
            var result = Cards[currentCard].CompareTo(other.Cards[currentCard]);

            if (result != 0)
            {
                return result;
            }
        }

        return 0;
    }

    public static bool operator <(Hand left, Hand right) =>
        ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(Hand left, Hand right) =>
        ReferenceEquals(left, null) ||
        left.CompareTo(right) <= 0;

    public static bool operator >(Hand left, Hand right) =>
        !ReferenceEquals(left, null) &&
        left.CompareTo(right) > 0;

    public static bool operator >=(Hand left, Hand right) =>
        ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;
}

namespace AoC.Extensions;

public static class ListExtensions
{
    public static bool CollectionsEqual<T>(this IEnumerable<T> left, IEnumerable<T> right)
    {
        using var leftEnumerator = left.GetEnumerator();
        using var rightEnumerator = right.GetEnumerator();

        var comparer = EqualityComparer<T>.Default;

        do
        {
            var leftNext = leftEnumerator.MoveNext();
            var rightNext = rightEnumerator.MoveNext();

            if (!leftNext)
            {
                return !rightNext;
            }

            if (!rightNext)
            {
                return false;
            }

            if (!comparer.Equals(leftEnumerator.Current, rightEnumerator.Current))
            {
                return false;
            }
        } while (true);
    }
}

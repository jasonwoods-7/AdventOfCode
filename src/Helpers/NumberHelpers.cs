namespace AoC.Helpers;

public static class NumberHelpers
{
    public static long GreatestCommonDivisor(long a, long b) =>
        b == 0
            ? a
            : GreatestCommonDivisor(b, a % b);

    public static long LeastCommonMultiple(long a, long b) =>
        a * b / GreatestCommonDivisor(a, b);
}

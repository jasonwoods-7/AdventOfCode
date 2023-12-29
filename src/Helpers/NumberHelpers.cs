namespace AoC.Helpers;

public static class NumberHelpers
{
    public static long GreatestCommonDivisor(long a, long b) =>
        b == 0
            ? a
            : GreatestCommonDivisor(b, a % b);

    public static long LeastCommonMultiple(long a, long b) =>
        a * b / GreatestCommonDivisor(a, b);

    public static bool Coprime(long a, long b) =>
        GreatestCommonDivisor(a, b) == 1;

    public static long ModInverse(long num, long mod)
    {
        if (mod < 0)
        {
            mod = -mod;
        }

        if (num < 0)
        {
            num = mod - (-num % mod);
        }

        Debug.Assert(Coprime(num, mod), "numbers must be co-prime");

        var (t, nt) = (0L, 1L);
        var (r, nr) = (mod, num % mod);

        while (nr != 0)
        {
            var q = r / nr;
            (t, nt) = (nt, t - (q * nt));
            (r, nr) = (nr, r - (q * nr));
        }

        return r > 1 // not co-prime
            ? -1
            : t < 0
                ? t + mod
                : t;
    }

    public static long ChineseRemainderTheorem(long[] numbers, long[] mods)
    {
        Debug.Assert(numbers.Length == mods.Length, "Lengths of arrays are not equal");
        Debug.Assert(numbers.Aggregate(GreatestCommonDivisor) == 1, "numbers are not co-prime");

        var prod = numbers.Product();
        var sm = 0L;

        for (var i = 0; i < numbers.Length; i++)
        {
            var p = prod / numbers[i];
            sm += mods[i] * ModInverse(p, numbers[i]) * p;
        }

        return sm % prod;
    }
}

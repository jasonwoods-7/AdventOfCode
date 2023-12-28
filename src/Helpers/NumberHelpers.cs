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

    public static long ChineseRemainderTheorem(long[] n, long[] a)
    {
#if DEBUG
        Debug.Assert(n.Length == a.Length);
#endif

        var prod = n.Product();
        var sm = 0L;

        for (var i = 0; i < n.Length; i++)
        {
            var p = prod / n[i];
            sm += a[i] * ModInverse(p, n[i]) * p;
        }

        return sm % prod;
    }
}

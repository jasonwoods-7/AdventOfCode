namespace AoC.Y2022.Day11;

sealed class Reducer
{
    readonly long _divisor;

    public Reducer(long divisor)
    {
        _divisor = divisor;
        Reduce = ReduceInternal;
    }

    public Func<long, long> Reduce { get; }

    long ReduceInternal(long value) => Math.DivRem(value, _divisor).Remainder;
}

namespace AoC.Y2022.Day11;

sealed class Reducer
{
    readonly long _divisor;

    public Reducer(long divisor) => _divisor = divisor;

    public long Reduce(long value) => Math.DivRem(value, _divisor).Remainder;
}

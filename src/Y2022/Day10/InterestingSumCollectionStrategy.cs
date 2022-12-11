namespace AoC.Y2022.Day10;

sealed class InterestingSumCollectionStrategy : ICollectionStrategy
{
    public int InterestingSum { get; private set; }

    public void Collect(int currentCycle, int xRegister) => InterestingSum += currentCycle switch
    {
        20 or 60 or 100 or 140 or 180 or 220 => currentCycle * xRegister,
        _ => 0
    };
}

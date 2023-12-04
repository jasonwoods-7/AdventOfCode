namespace AoC.Types;

public interface IMonoid<TSelf>
    where TSelf : IMonoid<TSelf>
{
    static abstract TSelf Empty();

    TSelf Append(TSelf other);
}

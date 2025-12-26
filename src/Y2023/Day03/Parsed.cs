namespace AoC.Y2023.Day03;

public record Parsed(ImmutableList<Number> Numbers, ImmutableList<Symbol> Symbols) : IMonoid<Parsed>
{
    public static Parsed Empty() => new(ImmutableList<Number>.Empty, ImmutableList<Symbol>.Empty);

    public Parsed Append(Parsed other) =>
        new(Numbers.AddRange(other.Numbers), Symbols.AddRange(other.Symbols));
}

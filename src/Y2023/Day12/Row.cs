namespace AoC.Y2023.Day12;

public record Row(string Springs, ImmutableStack<int> Groups)
{
    public Row Unfold(int multiplier) =>
        new(
            string.Join('?', Enumerable.Repeat(Springs, multiplier)),
            Enumerable
                .Repeat(Groups, multiplier)
                .SelectMany(SuperEnumerable.Identity)
                .Reverse()
                .Apply(ImmutableStack.CreateRange)
        );
}

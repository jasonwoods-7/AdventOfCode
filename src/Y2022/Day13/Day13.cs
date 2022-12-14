namespace AoC.Y2022.Day13;

public class Day13 : IAoCRunner<IEnumerable<(Packet, Packet)>, int>
{
    public IEnumerable<(Packet, Packet)> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Split(l => l == string.Empty)
        .Select(ls => ls
            .Fold((p1, p2) => (Packet.ParsePacket(p1), Packet.ParsePacket(p2))));

    public int RunPart1(IEnumerable<(Packet, Packet)> input)
    {
        var comparer = new PacketComparer();

        return input
            .Index()
            .Choose(kvp =>
            {
                var (p1, p2) = kvp.item;

                return (comparer.Compare(p1, p2) == -1, kvp.index + 1);
            })
            .Sum();
    }

    public int RunPart2(IEnumerable<(Packet, Packet)> input)
    {
        var decoder1 = Packet.ParsePacket("[[2]]");
        var decoder2 = Packet.ParsePacket("[[6]]");
        var comparer = new PacketComparer();

        return input
            .Append((decoder1, decoder2))
            .SelectMany(t => new[] { t.Item1, t.Item2 })
            .Order(comparer)
            .Index()
            .Choose(p => (ReferenceEquals(p.item, decoder1) || ReferenceEquals(p.item, decoder2), p.index + 1))
            .Product();
    }
}

namespace AoC.Y2022.Day13;

public class PacketComparer : IComparer<Packet>
{
    /// <inheritdoc />
    public int Compare(Packet? x, Packet? y)
    {
        Debug.Assert(x is not null);
        Debug.Assert(y is not null);

        using var xDataEnumerator = x.Data.GetEnumerator();
        using var yDataEnumerator = y.Data.GetEnumerator();

        while (xDataEnumerator.MoveNext())
        {
            if (!yDataEnumerator.MoveNext())
            {
                return 1;
            }

            var xCurrent = xDataEnumerator.Current;
            var yCurrent = yDataEnumerator.Current;

            var result = (xCurrent.IsFirst, yCurrent.IsFirst) switch
            {
                (true, true) => xCurrent.First.CompareTo(yCurrent.First),
                (true, false) => Compare(
                    Packet.ParsePacket($"[{xCurrent.First}]"),
                    yCurrent.Second
                ),
                (false, true) => Compare(
                    xCurrent.Second,
                    Packet.ParsePacket($"[{yCurrent.First}]")
                ),
                _ => Compare(xCurrent.Second, yCurrent.Second),
            };

            if (result != 0)
            {
                return result;
            }
        }

        return yDataEnumerator.MoveNext() ? -1 : 0;
    }
}

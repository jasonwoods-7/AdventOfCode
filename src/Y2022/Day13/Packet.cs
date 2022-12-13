using AnyOfTypes;

namespace AoC.Y2022.Day13;

public class Packet
{
    public IReadOnlyList<AnyOf<int, Packet>> Data { get; }

    Packet(IReadOnlyList<AnyOf<int, Packet>> data) =>
        Data = data;

    public static Packet ParsePacket(ReadOnlySpan<char> packet)
    {
        Debug.Assert(packet[0] == '[');
        Debug.Assert(packet[^1] == ']');

        var data = new List<AnyOf<int, Packet>>();

        for (var counter = 1; counter < packet.Length - 1; ++counter)
        {
            if (packet[counter] == '[')
            {
                var packetLen = PacketLength(packet[counter..]);

                data.Add(ParsePacket(packet[counter..(counter + packetLen)]));

                counter += packetLen - 1;
            }
            else if (char.IsDigit(packet[counter]))
            {
                var numStr = new string(packet[counter], 1);

                while (char.IsDigit(packet[counter + 1]))
                {
                    numStr += packet[++counter];
                }

                data.Add(int.Parse(numStr, CultureInfo.CurrentCulture));
            }
        }

        return new Packet(data);
    }

    static int PacketLength(ReadOnlySpan<char> packet)
    {
        var level = 0;

        for (var counter = 0; counter < packet.Length; ++counter)
        {
            if (packet[counter] == '[')
            {
                level++;
            }
            else if (packet[counter] == ']')
            {
                level--;

                if (level == 0)
                {
                    return counter + 1;
                }
            }
        }

        throw new InvalidOperationException();
    }
}

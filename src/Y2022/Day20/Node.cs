namespace AoC.Y2022.Day20;

[DebuggerDisplay("{Value}")]
sealed class Node
{
    public long Value { get; }

    public Node(long value) => Value = value;

    public Node Next { get; set; } = null!;

    public Node Previous { get; set; } = null!;

    public void Mix(long length) =>
        InternalMix(this, this, Math.Sign(Value), Math.DivRem(Math.Abs(Value), length).Remainder);

    static void InternalMix(Node source, Node destination, int direction, long remaining)
    {
        if (direction == 0)
        {
            return;
        }

        while (true)
        {
            if (remaining-- == 0)
            {
                if (direction == -1)
                {
                    MoveBefore(source, destination);
                    break;
                }

                MoveAfter(source, destination);
                break;
            }

            do
            {
                destination = direction == -1 ? destination.Previous : destination.Next;
            } while (destination == source);
        }
    }

    static void MoveAfter(Node source, Node destination)
    {
        source.Previous.Next = source.Next;
        source.Next.Previous = source.Previous;

        source.Previous = destination;
        source.Next = destination.Next;

        destination.Next.Previous = source;
        destination.Next = source;
    }

    static void MoveBefore(Node source, Node destination)
    {
        source.Previous.Next = source.Next;
        source.Next.Previous = source.Previous;

        source.Previous = destination.Previous;
        source.Next = destination;

        destination.Previous.Next = source;
        destination.Previous = source;
    }
}

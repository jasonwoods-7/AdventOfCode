using System.Collections;

namespace AoC.Y2022.Day20;

class CircularList : IEnumerable<Node>
{
    readonly Node _head;

    public CircularList(IEnumerable<long> items)
    {
        using var itemEnumerator = items.GetEnumerator();

        itemEnumerator.MoveNext();
        _head = new Node(itemEnumerator.Current);

        var current = _head;

        while (itemEnumerator.MoveNext())
        {
            var next = new Node(itemEnumerator.Current);
            current.Next = next;
            next.Previous = current;
            current = next;
        }

        _head.Previous = current;
        current.Next = _head;
    }

    public Node Find(long value)
    {
        var current = _head;

        while (current.Value != value)
        {
            current = current.Next;
        }

        return current;
    }

    /// <inheritdoc />
    public IEnumerator<Node> GetEnumerator() => new CircularListSnapshotEnumerator(_head);

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    sealed class CircularListSnapshotEnumerator : IEnumerator<Node>
    {
        readonly IEnumerator<Node> _enumerator;

        public CircularListSnapshotEnumerator(Node head)
        {
            var list = new List<Node>
            {
                head
            };

            var next = head.Next;

            while (next != head)
            {
                list.Add(next);
                next = next.Next;
            }

            _enumerator = list.GetEnumerator();
        }

        /// <inheritdoc />
        public bool MoveNext() => _enumerator.MoveNext();

        /// <inheritdoc />
        public void Reset() => _enumerator.Reset();

        /// <inheritdoc />
        public Node Current => _enumerator.Current;

        /// <inheritdoc />
        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public void Dispose() => _enumerator.Dispose();
    }
}

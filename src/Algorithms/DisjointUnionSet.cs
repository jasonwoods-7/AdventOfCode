using System.Collections;

namespace AoC.Algorithms;

[DebuggerTypeProxy(typeof(DisjointUnionSet<>.DebugView))]
public partial class DisjointUnionSet<T> : ICollection<T>
    where T : notnull
{
    readonly Dictionary<T, Node> _nodes;

    public DisjointUnionSet()
        : this([], EqualityComparer<T>.Default) { }

    public DisjointUnionSet(IEnumerable<T> values)
        : this(values, EqualityComparer<T>.Default) { }

    public DisjointUnionSet(IEnumerable<T> values, IEqualityComparer<T> comparer) =>
        _nodes = values.ToDictionary(k => k, v => new Node(v), comparer);

    public IEnumerator<T> GetEnumerator() => _nodes.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Add(T item)
    {
        if (Contains(item))
        {
            return false;
        }

        _nodes.Add(item, new Node(item));
        return true;
    }

    void ICollection<T>.Add(T item) => Add(item);

    public void Clear() => _nodes.Clear();

    public bool Contains(T item) => _nodes.ContainsKey(item);

    public int Count => _nodes.Count;

    public bool IsReadOnly => false;

    public bool IsEmpty => Count == 0;

    public bool Union(T valueA, T valueB) => _nodes[valueA].Union(_nodes[valueB]);

    public T Find(T value) => _nodes[value].Find().Value;

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) => throw new NotSupportedException();

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();
}

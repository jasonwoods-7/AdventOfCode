namespace AoC.Algorithms;

partial class DisjointUnionSet<T>
{
    class DebugView
    {
        readonly DisjointUnionSet<T> _set;

        public DebugView(DisjointUnionSet<T> set) => _set = set;

        public Node[] Nodes
        {
            get
            {
                var nodes = new Node[_set._nodes.Count];
                _set._nodes.Values.CopyTo(nodes, 0);
                return nodes;
            }
        }

        public Node[] Roots =>
            _set._nodes.Values.GroupBy(n => n.Find()).Select(g => g.First()).Distinct().ToArray();
    }
}

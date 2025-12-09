namespace AoC.Algorithms;

partial class DisjointUnionSet<T>
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    sealed class Node
    {
        public T Value { get; }
        Node Parent { get; set; }
        int Rank { get; set; }

        public Node(T value)
        {
            Value = value;
            Parent = this;
            Rank = 0;
        }

        public bool Union(Node other)
        {
            var parentA = Find();
            var parentB = other.Find();

            if (parentA == parentB)
            {
                return false;
            }

            if (parentA.Rank >= parentB.Rank)
            {
                if (parentA.Rank == parentB.Rank)
                {
                    parentA.Rank += 1;
                }

                parentB.Parent = parentA;
            }
            else
            {
                parentA.Parent = parentB;
            }

            return true;
        }

        public Node Find()
        {
            if (Parent == this)
            {
                return this;
            }

            return Parent = Parent.Find();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string DebuggerDisplay => $"Value: {Value}, Rank: {Rank}, Parent: {Parent.Value}";
    }
}

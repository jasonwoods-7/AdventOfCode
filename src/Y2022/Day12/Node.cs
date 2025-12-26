namespace AoC.Y2022.Day12;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Node
{
    (int, int) _position;

    public int Height { get; }

    public ISet<Node> AdjacentNodes { get; }

    public Node(int height)
    {
        Height = height;
        AdjacentNodes = new System.Collections.Generic.HashSet<Node>();
    }

    public Node AddPosition((int, int) position)
    {
        _position = position;
        return this;
    }

    string DebuggerDisplay => $"X: {_position.Item1}, Y: {_position.Item2}, Height: {Height}";
}

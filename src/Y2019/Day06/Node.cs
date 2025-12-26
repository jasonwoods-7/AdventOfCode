namespace AoC.Y2019.Day06;

public record Node(string Name)
{
    public Node? Parent { get; set; }
    public IList<Node> Children { get; } = new List<Node>();

    public long Height(long currentHeight = 0) =>
        Parent?.Height(currentHeight + 1) ?? currentHeight;
}

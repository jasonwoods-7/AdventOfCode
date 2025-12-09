namespace AoC.Y2019.Day06;

public record Node(string Name)
{
    public Node? Parent { get; set; }
    public IList<Node> Children { get; } = new List<Node>();

    public long Height(long currentHeight = 0) =>
        Parent?.Height(currentHeight + 1) ?? currentHeight;
}

public class Day06 : IAoCRunner<IReadOnlyDictionary<string, Node>, long>
{
    public IReadOnlyDictionary<string, Node> ParseInput(IEnumerable<string> puzzleInput)
    {
        var orbits = puzzleInput
            .Select(l => l.Split(')').Fold((parent, child) => (parent, child)))
            .ToList();

        var nodes = orbits
            .SelectMany(o => new[] { o.parent, o.child })
            .Distinct()
            .Select(n => new Node(n))
            .ToDictionary(n => n.Name);

        foreach (var (p, c) in orbits)
        {
            nodes[p].Children.Add(nodes[c]);
            nodes[c].Parent = nodes[p];
        }

        return nodes;
    }

    public long RunPart1(
        IReadOnlyDictionary<string, Node> input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) => input.Sum(n => n.Value.Height());

    public long RunPart2(
        IReadOnlyDictionary<string, Node> input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        return SuperEnumerable.GetShortestPathCost<Node, long>(
                input["YOU"],
                GetChildren,
                input["SAN"]
            ) - 2;

        static IEnumerable<(Node, long)> GetChildren(Node current, long cost)
        {
            if (current.Parent is not null)
            {
                yield return (current.Parent, cost + 1);
            }

            foreach (var child in current.Children)
            {
                yield return (child, cost + 1);
            }
        }
    }
}

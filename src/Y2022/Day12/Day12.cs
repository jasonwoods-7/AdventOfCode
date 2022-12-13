namespace AoC.Y2022.Day12;

public class Day12 : IAoCRunner<(Node, Node), int>
{
    public (Node, Node) ParseInput(IEnumerable<string> puzzleInput)
    {
        var startPos = new Node(1);
        var endPos = new Node(26);

        var grid = puzzleInput
            .SelectMany((s, y) => s.Select((c, x) => (c, x, y)))
            .ToDictionary(t => (t.x, t.y), t => (t.c switch
            {
                'S' => startPos,
                'E' => endPos,
                _ => new Node(t.c - ('a' - 1))
            }).AddPosition((t.x, t.y)));

        foreach (var ((x, y), node) in grid)
        {
            AddValidAdjacentIfExists(grid, (x - 1, y), node);
            AddValidAdjacentIfExists(grid, (x + 1, y), node);
            AddValidAdjacentIfExists(grid, (x, y - 1), node);
            AddValidAdjacentIfExists(grid, (x, y + 1), node);
        }

        return (startPos, endPos);

        static void AddValidAdjacentIfExists(Dictionary<(int, int), Node> grid, (int, int) key, Node current)
        {
            if (grid.TryGetValue(key, out var adjacent) &&
                adjacent.Height - current.Height <= 1)
            {
                current.AdjacentNodes.Add(adjacent);
            }
        }
    }

    public int RunPart1((Node, Node) input)
    {
        var (startPos, endPos) = input;

        return SuperEnumerable.GetShortestPathCost<Node, int>(
            startPos,
            static (n, c) => n.AdjacentNodes.Select(a => (a, c + 1)),
            endPos);
    }

    public int RunPart2((Node, Node) input)
    {
        var (startPos, endPos) = input;

        var visitedNodes = new System.Collections.Generic.HashSet<Node>();

        static IEnumerable<Node> PotentialStartPositions(Node currentNode,
            System.Collections.Generic.HashSet<Node> visitedNodes)
        {
            if (!visitedNodes.Add(currentNode))
            {
                yield break;
            }

            if (currentNode.Height == 1)
            {
                yield return currentNode;
            }

            foreach (var adjacent in currentNode
                .AdjacentNodes
                .SelectMany(n => PotentialStartPositions(n, visitedNodes)))
            {
                yield return adjacent;
            }
        }

        return PotentialStartPositions(startPos, visitedNodes).Min(n => Try(() =>
            SuperEnumerable.GetShortestPathCost<Node, int>(
                n,
                static (n, c) => n.AdjacentNodes.Select(a => (a, c + 1)),
                endPos))
            .IfFail(int.MaxValue));
    }
}

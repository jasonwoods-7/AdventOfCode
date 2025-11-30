namespace AoC.Y2022.Day08;

public class Day08 : IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int>
{
    public IReadOnlyList<IReadOnlyList<int>> ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Select(s => s.Select(c => c - '0').ToList()).ToList();

    public int RunPart1(IReadOnlyList<IReadOnlyList<int>> input)
    {
        // Assume square
        Debug.Assert(input.Count == input[0].Count);
        var side = input.Count;

        // add edges
        var seenTrees = Enumerable
            .Range(0, side)
            .SelectMany(x => new[] { (0, x), (side - 1, x), (x, 0), (x, side - 1) })
            .ToHashSet();

        var intComparer = Comparer<int>.Default;

        for (var x = 1; x < side - 1; ++x)
        {
            // seen from top
            for (int y = 1, tallestSeen = input[0][x]; y < side - 1 && tallestSeen != 9; ++y)
            {
                AddIfSeen(intComparer, seenTrees, (x, y), input[y][x], ref tallestSeen);
            }

            // seen from bottom
            for (int y = side - 2, tallestSeen = input[side - 1][x]; y > 0 && tallestSeen != 9; --y)
            {
                AddIfSeen(intComparer, seenTrees, (x, y), input[y][x], ref tallestSeen);
            }
        }

        for (var y = 1; y < side - 1; ++y)
        {
            // seen from left
            for (int x = 1, tallestSeen = input[y][0]; x < side - 1 && tallestSeen != 9; ++x)
            {
                AddIfSeen(intComparer, seenTrees, (x, y), input[y][x], ref tallestSeen);
            }

            // seen from right
            for (int x = side - 2, tallestSeen = input[y][side - 1]; x > 0 && tallestSeen != 9; --x)
            {
                AddIfSeen(intComparer, seenTrees, (x, y), input[y][x], ref tallestSeen);
            }
        }

        return seenTrees.Count;

        static void AddIfSeen(
            IComparer<int> comparer,
            ISet<(int, int)> seenTrees,
            (int, int) coords,
            int currentTree,
            ref int tallestSeen
        )
        {
            if (comparer.Compare(tallestSeen, currentTree) < 0)
            {
                seenTrees.Add(coords);
                tallestSeen = currentTree;
            }
        }
    }

    public int RunPart2(IReadOnlyList<IReadOnlyList<int>> input)
    {
        static (int, int, int, int) SeenFrom(
            IReadOnlyList<IReadOnlyList<int>> treeMap,
            int x,
            int y
        )
        {
            var side = treeMap.Count;

            if (x == 0 || y == 0 || x == side - 1 || y == side - 1)
            {
                return (0, 0, 0, 0);
            }

            var intComparer = Comparer<int>.Default;
            var selectedTree = treeMap[y][x];

            // seen above
            var seenAbove = 0;
            for (
                int count = y - 1, tallestSeen = treeMap[count][x];
                count >= 0 && tallestSeen != 9;
                --count, ++seenAbove
            )
            {
                tallestSeen = CheckIfSeen(intComparer, selectedTree, treeMap[count][x]);
            }

            // seen left
            var seenLeft = 0;
            for (
                int count = x + 1, tallestSeen = treeMap[y][count];
                count < side && tallestSeen != 9;
                ++count, ++seenLeft
            )
            {
                tallestSeen = CheckIfSeen(intComparer, selectedTree, treeMap[y][count]);
            }

            // seen right
            var seenRight = 0;
            for (
                int count = x - 1, tallestSeen = treeMap[y][count];
                count >= 0 && tallestSeen != 9;
                --count, ++seenRight
            )
            {
                tallestSeen = CheckIfSeen(intComparer, selectedTree, treeMap[y][count]);
            }

            // seen below
            var seenBelow = 0;
            for (
                int count = y + 1, tallestSeen = treeMap[count][x];
                count < side && tallestSeen != 9;
                ++count, ++seenBelow
            )
            {
                tallestSeen = CheckIfSeen(intComparer, selectedTree, treeMap[count][x]);
            }

            return (seenAbove, seenLeft, seenRight, seenBelow);

            static int CheckIfSeen(IComparer<int> comparer, int selectedTree, int currentTree) =>
                comparer.Compare(selectedTree, currentTree) switch
                {
                    1 => currentTree,
                    _ => 9,
                };
        }

        return input
            .SelectMany((i, y) => i.Select((_, x) => SeenFrom(input, x, y)))
            .Select(t => t.Item1 * t.Item2 * t.Item3 * t.Item4)
            .OrderDescending()
            .First();
    }
}

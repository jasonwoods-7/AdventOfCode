namespace AoC.Y2024.Day05;

sealed class UpdateComparer(IReadOnlyList<Rule> rules) : IComparer<int>
{
    public int Compare(int x, int y) =>
        rules
            .Choose(r =>
                r switch
                {
                    _ when r.Before == x && r.After == y => (true, -1),
                    _ when r.Before == y && r.After == x => (true, 1),
                    _ => (false, 0),
                }
            )
            .First();
}

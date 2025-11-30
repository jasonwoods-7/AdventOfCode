namespace AoC.Y2022.Day18;

public class Day18 : IAoCRunner<LavaDroplet, int>
{
    public LavaDroplet ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Select(l => l.FindNumbers<int>().Fold((x, y, z) => new Coord3d(x, y, z)))
            .Apply(ps => new LavaDroplet(ps));

    public int RunPart1(LavaDroplet input) => input.CountFaces();

    public int RunPart2(LavaDroplet input) => input.ExternalArea();
}

using System.Text;

namespace AoC.Y2022.Day17;

partial class Day17
{
    // ReSharper disable once UnusedMember.Local
    static string RenderGrid(IReadOnlySet<Coord> grid)
    {
        var minY = grid.MinBy(c => c.Y).Y;
        var builder = new StringBuilder((int)(9 * (-minY + 1)));

        for (var y = minY; y < 0; ++y)
        {
            for (var x = 0; x < 7; ++x)
            {
                builder.Append(grid.Contains(new Coord(x, y)) ? '#' : ' ');
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }
}

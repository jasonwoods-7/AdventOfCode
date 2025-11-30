namespace AoC.Y2022.Day18;

public sealed class LavaDroplet
{
    readonly IReadOnlySet<Coord3d> _points;

    public LavaDroplet(IEnumerable<Coord3d> points) => _points = points.ToHashSet();

    public int CountFaces() =>
        _points.SelectMany(c => c.Neighbors()).Count(c => !_points.Contains(c));

    public int ExternalArea()
    {
        var flood = Flood();

        return _points.SelectMany(c => c.Neighbors()).Count(c => flood.Contains(c));
    }

    IReadOnlySet<Coord3d> Flood()
    {
        var bounds = FindBounds();

        var flood = new System.Collections.Generic.HashSet<Coord3d> { bounds.Lower };

        var nextLocations = new Queue<Coord3d>();
        nextLocations.Enqueue(bounds.Lower);

        while (nextLocations.Count != 0)
        {
            var current = nextLocations.Dequeue();
            foreach (var neighbor in current.Neighbors())
            {
                if (
                    !flood.Contains(neighbor)
                    && bounds.WithinBounds(neighbor)
                    && !_points.Contains(neighbor)
                )
                {
                    flood.Add(neighbor);
                    nextLocations.Enqueue(neighbor);
                }
            }
        }

        return flood;
    }

    Bounds FindBounds()
    {
        var minX = _points.MinBy(c => c.X).X - 1;
        var maxX = _points.MaxBy(c => c.X).X + 1;

        var minY = _points.MinBy(c => c.Y).Y - 1;
        var maxY = _points.MaxBy(c => c.Y).Y + 1;

        var minZ = _points.MinBy(c => c.Z).Z - 1;
        var maxZ = _points.MaxBy(c => c.Z).Z + 1;

        return new Bounds(new Coord3d(minX, minY, minZ), new Coord3d(maxX, maxY, maxZ));
    }
}

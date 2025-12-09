namespace AoC.Extensions;

public static class Coord3dExtensions
{
    extension(Coord3d source)
    {
        public IEnumerable<Coord3d> Adjacent()
        {
            var dimensions = new[] { -1, 0, 1 };

            foreach (var z in dimensions)
            {
                foreach (var y in dimensions)
                {
                    foreach (var x in dimensions)
                    {
                        if ((x, y, z) == default)
                        {
                            continue;
                        }

                        yield return new Coord3d(source.X + x, source.Y + y, source.Z + z);
                    }
                }
            }
        }

        public IEnumerable<Coord3d> Neighbors()
        {
            yield return source with
            {
                Y = source.Y - 1,
            }; // above
            yield return source with
            {
                X = source.X - 1,
            }; // left
            yield return source with
            {
                X = source.X + 1,
            }; // right
            yield return source with
            {
                Y = source.Y + 1,
            }; // below
            yield return source with
            {
                Z = source.Z - 1,
            }; // plane-above
            yield return source with
            {
                Z = source.Z + 1,
            }; // plane-below
        }

        public double DistanceTo(Coord3d other) =>
            Math.Sqrt(
                Math.Pow(source.X - other.X, 2)
                    + Math.Pow(source.Y - other.Y, 2)
                    + Math.Pow(source.Z - other.Z, 2)
            );
    }
}

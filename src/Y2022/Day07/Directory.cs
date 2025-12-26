namespace AoC.Y2022.Day07;

public class Directory(string name, Directory? parent)
{
    public string Name { get; } = name;

    public Directory? Parent { get; } = parent;

    public IList<Directory> Subdirectories { get; } = new List<Directory>();

    public IList<(string name, int size)> Files { get; } = new List<(string, int)>();

    public int CalculateSize() =>
        Files.Sum(f => f.size) + Subdirectories.Sum(d => d.CalculateSize());
}

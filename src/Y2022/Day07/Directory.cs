namespace AoC.Y2022.Day07;

public class Directory
{
    public Directory(string name, Directory? parent)
    {
        Name = name;
        Parent = parent;
        Subdirectories = new List<Directory>();
        Files = new List<(string, int)>();
    }

    public string Name { get; }

    public Directory? Parent { get; }

    public List<Directory> Subdirectories { get; }

    public List<(string name, int size)> Files { get; }

    public int CalculateSize() =>
        Files.Sum(f => f.size) +
        Subdirectories.Sum(d => d.CalculateSize());
}

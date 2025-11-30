namespace AoC.Y2022.Day07;

public class Day07 : IAoCRunner<Directory, int>
{
    public Directory ParseInput(IEnumerable<string> puzzleInput)
    {
        var root = new Directory("/", null);
        var currentDirectory = root;

        foreach (var command in puzzleInput.Skip(1))
        {
            if (command == "$ cd ..")
            {
                currentDirectory = currentDirectory.Parent!;
            }
            else if (command.StartsWith("$ cd ", StringComparison.Ordinal))
            {
                var newDirectory = command[5..];
                currentDirectory = currentDirectory.Subdirectories.First(d =>
                    d.Name == newDirectory
                );
            }
            else if (command == "$ ls") { }
            else if (command.StartsWith("dir ", StringComparison.Ordinal))
            {
                var name = command[4..];
                currentDirectory.Subdirectories.Add(new Directory(name, currentDirectory));
            }
            else
            {
                var split = command.Split(" ");
                currentDirectory.Files.Add(
                    (split[1], int.Parse(split[0], CultureInfo.CurrentCulture))
                );
            }
        }

        return root;
    }

    public int RunPart1(Directory input) => SubdirectorySizes(input).Where(s => s < 100_000).Sum();

    public int RunPart2(Directory input)
    {
        var freeSpace = 70_000_000 - input.CalculateSize();
        var requiredSpace = 30_000_000 - freeSpace;

        return SubdirectorySizes(input).Where(s => s >= requiredSpace).Order().First();
    }

    static IEnumerable<int> SubdirectorySizes(Directory current)
    {
        yield return current.CalculateSize();

        foreach (var sub in current.Subdirectories.SelectMany(SubdirectorySizes))
        {
            yield return sub;
        }
    }
}

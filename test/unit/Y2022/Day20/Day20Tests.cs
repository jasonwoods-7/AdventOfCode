namespace AoC.Tests.Y2022.Day20;

public class Day20Tests
{
    // [Fact]
    public static void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(0);
    }

    // [Fact]
    public static void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(0);
    }

    // [Fact]
    public static void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(0);
    }

    // [Fact]
    public static void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(0);
    }

    static IAoCRunner<IEnumerable<string>, int> CreateRunner() => new AoC.Y2022.Day20.Day20();
}

namespace AoC.Tests.Y2023.Day09;

public class Day09Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(114);
    }

    [SkippableFact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(1_789_635_132);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(2);
    }

    [SkippableFact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(913);
    }

    static IAoCRunner<IReadOnlyList<IReadOnlyList<int>>, int> CreateRunner() => new AoC.Y2023.Day09.Day09();
}

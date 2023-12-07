namespace AoC.Tests.Y2023.Day06;

public class Day06Tests
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
        result.Should().Be(288L);
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
        result.Should().Be(303_600L);
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
        result.Should().Be(71_503L);
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
        result.Should().Be(23_654_842L);
    }

    static IAoCRunner<IReadOnlyList<AoC.Y2023.Day06.Race>, long> CreateRunner() => new AoC.Y2023.Day06.Day06();
}

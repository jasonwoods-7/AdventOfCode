using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day12;

public class Day12Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(false);

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(21);
    }

    [SkippableFact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner(false);

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(7_169);
    }

    [Fact(Skip = "Not optimized")]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner(true);

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(525_152);
    }

    [Theory]
    [InlineData(".# 1", 1)]
    [InlineData("???.### 1,1,3", 1)]
    public void AdditionalExamples_Part2(string notes, int expected)
    {
        // Arrange
        var runner = CreateRunner(true);

        var input = runner.ParseInput(notes);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(expected);
    }

    static IAoCRunner<IReadOnlyList<AoC.Y2023.Day12.Row>, int> CreateRunner(bool part2) => new AoC.Y2023.Day12.Day12(part2);
}

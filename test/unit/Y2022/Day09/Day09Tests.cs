using AoC.Y2022.Day09;

namespace AoC.Tests.Y2022.Day09;

public class Day09Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(13);
    }

    [Fact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(5_779);
    }

    [Theory]
    [InlineData("example.txt", 1)]
    [InlineData("example2.txt", 36)]
    public void Example_Part2(string fileName, int expected)
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile(fileName));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(2_331);
    }

    static IAoCRunner<IEnumerable<(Direction, int)>, int> CreateRunner() => new AoC.Y2022.Day09.Day09();
}

using AnyOfTypes;
using AoC.Y2022.Day10;

namespace AoC.Tests.Y2022.Day10;

[UsesVerify]
public class Day10Tests
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
        actual.IsFirst.Should().BeTrue();
        actual.First.Should().Be(13_140);
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
        actual.IsFirst.Should().BeTrue();
        actual.First.Should().Be(15_360);
    }

    [Fact]
    public Task Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.IsSecond.Should().BeTrue();
        return Verify(actual.Second);
    }

    [Fact]
    public Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.IsSecond.Should().BeTrue();
        return Verify(actual.Second);
    }

    static IAoCRunner<IEnumerable<IInstruction>, AnyOf<int, string>> CreateRunner() => new AoC.Y2022.Day10.Day10();
}

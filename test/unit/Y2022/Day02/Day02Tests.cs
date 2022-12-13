namespace AoC.Tests.Y2022.Day02;

public class Day02Tests
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
        actual.Should().Be(15);
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
        actual.Should().Be(13_809);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(12);
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
        actual.Should().Be(12_316);
    }

    static IAoCRunner<IEnumerable<string>, int> CreateRunner() => new AoC.Y2022.Day02.Day02();
}

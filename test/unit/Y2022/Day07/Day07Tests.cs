namespace AoC.Tests.Y2022.Day07;

public class Day07Tests
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
        actual.Should().Be(95_437);
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
        actual.Should().Be(1_428_881);
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
        actual.Should().Be(24_933_642);
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
        actual.Should().Be(10_475_598);
    }

    static IAoCRunner<AoC.Y2022.Day07.Directory, int> CreateRunner() => new AoC.Y2022.Day07.Day07();
}

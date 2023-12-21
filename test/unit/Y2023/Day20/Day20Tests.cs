namespace AoC.Tests.Y2023.Day20;

public class Day20Tests
{
    [Theory]
    [InlineData("example1.txt", 32_000_000L)]
    [InlineData("example2.txt", 11_687_500L)]
    public void Example_Part1(string fileName, long expected)
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile(fileName));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(expected);
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
        result.Should().Be(825_167_435L);
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
        result.Should().Be(225_514_321_828_633L);
    }

    static IAoCRunner<IReadOnlyDictionary<string, AoC.Y2023.Day20.Module>, long> CreateRunner() => new AoC.Y2023.Day20.Day20();
}

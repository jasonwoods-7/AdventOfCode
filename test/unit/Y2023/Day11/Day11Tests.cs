namespace AoC.Tests.Y2023.Day11;

public class Day11Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(1L);

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(374L);
    }

    [SkippableFact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner(1L);

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(9_947_476L);
    }

    [Theory]
    [InlineData(10L, 1_030L)]
    [InlineData(100L, 8_410L)]
    public void Example_Part2(long multiplier, long expected)
    {
        // Arrange
        var runner = CreateRunner(multiplier - 1L);

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(expected);
    }

    [SkippableFact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner(999_999L);

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(519_939_907_614L);
    }

    static IAoCRunner<AoC.Y2023.Day11.Galaxy, long> CreateRunner(long multiplier) => new AoC.Y2023.Day11.Day11(multiplier);
}

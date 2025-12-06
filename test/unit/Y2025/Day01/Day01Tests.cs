using AoC.Tests.Extensions;

namespace AoC.Tests.Y2025.Day01;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day01Tests(ITestOutputHelper outputHelper) : AoCRunnerTests<AoC.Y2025.Day01.Day01>
{
    [Fact]
    public async Task Part1Example()
    {
        // Arrange
        using var loggerFactory = outputHelper.CreateLoggerFactory();
        var logger = loggerFactory.CreateLogger<AoC.Y2025.Day01.Day01>();

        var runner = CreateRunner(logger);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(3);
    }

    public override async Task Part1()
    {
        // Arrange
        using var loggerFactory = outputHelper.CreateLoggerFactory();
        var logger = loggerFactory.CreateLogger<AoC.Y2025.Day01.Day01>();

        var runner = CreateRunner(logger);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(1_152);
    }

    [Fact]
    public async Task Part2Example()
    {
        // Arrange
        using var loggerFactory = outputHelper.CreateLoggerFactory();
        var logger = loggerFactory.CreateLogger<AoC.Y2025.Day01.Day01>();

        var runner = CreateRunner(logger);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(6);
    }

    [Theory]
    [InlineData("L150", 2)]
    [InlineData("R150", 2)]
    [InlineData(
        """
            L50
            R101
            """,
        2
    )]
    [InlineData(
        """
            R50
            R50
            L50
            L50
            R75
            L50
            L25
            L75
            R50
            """,
        6
    )]
    public void Part2EdgeCases(string rawInput, int expected)
    {
        // Arrange
        using var loggerFactory = outputHelper.CreateLoggerFactory();
        var logger = loggerFactory.CreateLogger<AoC.Y2025.Day01.Day01>();

        var runner = CreateRunner(logger);

        var input = runner.ParseInput(rawInput);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(expected);
    }

    public override async Task Part2()
    {
        // Arrange
        using var loggerFactory = outputHelper.CreateLoggerFactory();
        var logger = loggerFactory.CreateLogger<AoC.Y2025.Day01.Day01>();

        var runner = CreateRunner(logger);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(6_671);
    }
}

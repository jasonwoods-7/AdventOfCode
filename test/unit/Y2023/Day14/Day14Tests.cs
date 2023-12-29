namespace AoC.Tests.Y2023.Day14;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day14Tests(ITestOutputHelper outputHelper) : AoCRunnerTests<AoC.Y2023.Day14.Day14>
{
    readonly ILoggerFactory _loggerFactory = outputHelper.CreateLoggerFactory();

    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day14.Day14>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(136);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day14.Day14>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(108_935L);
    }

    [Fact]
    public async Task Example_Part2()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day14.Day14>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(64);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day14.Day14>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(100_876L);
    }
}

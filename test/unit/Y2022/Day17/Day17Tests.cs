namespace AoC.Tests.Y2022.Day17;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day17Tests(ITestOutputHelper outputHelper) : AoCRunnerTests<AoC.Y2022.Day17.Day17>
{
    readonly ILoggerFactory _loggerFactory = outputHelper.CreateLoggerFactory();

    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2022.Day17.Day17>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(3_068);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2022.Day17.Day17>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(3_163);
    }

    [Fact]
    public async Task Example_Part2()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2022.Day17.Day17>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(1_514_285_714_288);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2022.Day17.Day17>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(1_560_932_944_615);
    }
}

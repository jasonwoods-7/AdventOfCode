using AnyOfTypes;
using AoC.Y2022.Day15;

namespace AoC.Tests.Y2022.Day15;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day15Tests : AoCRunnerTests<AoC.Y2022.Day15.Day15>
{
    readonly ILoggerFactory _loggerFactory = OutputHelpers.CreateLoggerFactory();

    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        AnyOf<Part1Data, Part2Data> data = new Part1Data(10);
        var runner = CreateRunner(data, _loggerFactory.CreateLogger<AoC.Y2022.Day15.Day15>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(26);
    }

    public override async Task Part1()
    {
        // Arrange
        AnyOf<Part1Data, Part2Data> data = new Part1Data(2_000_000);
        var runner = CreateRunner(data, _loggerFactory.CreateLogger<AoC.Y2022.Day15.Day15>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(4_876_693);
    }

    [Fact]
    public async Task Example_Part2()
    {
        // Arrange
        AnyOf<Part1Data, Part2Data> data = new Part2Data(20);
        var runner = CreateRunner(data, _loggerFactory.CreateLogger<AoC.Y2022.Day15.Day15>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(56_000_011);
    }

    public override async Task Part2()
    {
        // Arrange
        AnyOf<Part1Data, Part2Data> data = new Part2Data(4_000_000);
        var runner = CreateRunner(data, _loggerFactory.CreateLogger<AoC.Y2022.Day15.Day15>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(11_645_454_855_041);
    }
}

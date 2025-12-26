namespace AoC.Tests.Y2025.Day12;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day12Tests : AoCRunnerTests<AoC.Y2025.Day12.Day12>
{
    [Fact]
    public async Task Part1Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(1);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(505);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(-1);
    }
}

namespace AoC.Tests.Y2024.Day06;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public class Day06Tests : AoCRunnerTests<AoC.Y2024.Day06.Day06>
{
    [Fact]
    public async Task Part1Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(41);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(5_318);
    }

    [Fact]
    public async Task Part2Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(6);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(1_831);
    }
}

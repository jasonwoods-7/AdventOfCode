namespace AoC.Tests.Y2024.Day04;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public class Day04Tests : AoCRunnerTests<AoC.Y2024.Day04.Day04>
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
        result.ShouldBe(18);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(2_344);
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
        result.ShouldBe(9);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(1_815);
    }
}

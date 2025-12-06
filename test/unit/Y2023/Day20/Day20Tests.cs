namespace AoC.Tests.Y2023.Day20;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day20Tests : AoCRunnerTests<AoC.Y2023.Day20.Day20>
{
    [Theory]
    [InlineData("example1.txt", 32_000_000L)]
    [InlineData("example2.txt", 11_687_500L)]
    public async Task Example_Part1(string fileName, long expected)
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync(fileName));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(expected);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(825_167_435L);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(225_514_321_828_633L);
    }
}

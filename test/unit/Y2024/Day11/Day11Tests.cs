namespace AoC.Tests.Y2024.Day11;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public class Day11Tests : AoCRunnerTests<AoC.Y2024.Day11.Day11>
{
    [Fact]
    public void Part1Example()
    {
        // Arrange
        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();
        var runner = CreateRunner();

        var input = runner.ParseInput(["125 17"]);

        // Act
        var result = runner.RunPart1(input, cancellationToken: cts.Token);

        // Assert
        result.ShouldBe(55_312);
    }

    public override async Task Part1()
    {
        // Arrange
        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input, cancellationToken: cts.Token);

        // Assert
        result.ShouldBe(199_946);
    }

    public override async Task Part2()
    {
        // Arrange
        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input, cancellationToken: cts.Token);

        // Assert
        result.ShouldBe(237_994_815_702_032L);
    }
}

namespace AoC.Tests.Y2025.Day11;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day11Tests : AoCRunnerTests<AoC.Y2025.Day11.Day11>
{
    [Fact]
    public async Task Part1Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();

        // Act
        var actual = runner.RunPart1(input, cancellationToken: cts.Token);

        // Assert
        actual.ShouldBe(5);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();

        // Act
        var actual = runner.RunPart1(input, cancellationToken: cts.Token);

        // Assert
        actual.ShouldBe(543);
    }

    [Fact]
    public async Task Part2Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example2.txt"));

        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();

        // Act
        var actual = runner.RunPart2(input, cancellationToken: cts.Token);

        // Assert
        actual.ShouldBe(2);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();

        // Act
        var actual = runner.RunPart2(input, cancellationToken: cts.Token);

        // Assert
        actual.ShouldBe(479_511_112_939_968L);
    }
}

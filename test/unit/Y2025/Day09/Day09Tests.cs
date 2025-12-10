namespace AoC.Tests.Y2025.Day09;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day09Tests : AoCRunnerTests<AoC.Y2025.Day09.Day09>
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
        actual.ShouldBe(50);
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
        actual.ShouldBe(4_754_955_192L);
    }

    [Fact]
    public async Task Part2Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        using var cts = TokenSourceHelpers.CreateDefaultTokenSource();

        // Act
        var actual = runner.RunPart2(input, cancellationToken: cts.Token);

        // Assert
        actual.ShouldBe(24);
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
        actual.ShouldBe(1_568_849_600L);
    }
}

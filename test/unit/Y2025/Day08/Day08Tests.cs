namespace AoC.Tests.Y2025.Day08;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day08Tests : AoCRunnerTests<AoC.Y2025.Day08.Day08>
{
    [Fact]
    public async Task Part1Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input, [10], TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBe(40);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input, [1_000], TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBe(75_680);
    }

    [Fact]
    public async Task Part2Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(
            input,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Assert
        result.ShouldBe(25_272);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(
            input,
            cancellationToken: TestContext.Current.CancellationToken
        );

        // Assert
        result.ShouldBe(8_995_844_880L);
    }
}

namespace AoC.Tests.Y2022.Day07;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day07Tests : AoCRunnerTests<AoC.Y2022.Day07.Day07>
{
    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(95_437);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(1_428_881);
    }

    [Fact]
    public async Task Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(24_933_642);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(10_475_598);
    }
}

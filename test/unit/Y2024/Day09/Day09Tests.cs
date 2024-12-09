namespace AoC.Tests.Y2024.Day09;

[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public class Day09Tests : AoCRunnerTests<AoC.Y2024.Day09.Day09>
{
    [Fact]
    public void Part1Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(["2333133121414131402"]);

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(1_928);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(6_353_658_451_014L);
    }

    [Fact]
    public void Part2Example()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(["2333133121414131402"]);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(2_858);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(6_382_582_136_592L);
    }
}

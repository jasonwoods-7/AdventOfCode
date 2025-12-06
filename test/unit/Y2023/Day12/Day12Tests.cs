using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day12;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day12Tests : AoCRunnerTests<AoC.Y2023.Day12.Day12>
{
    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(21);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(7_169);
    }

    [Fact]
    public async Task Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(525_152);
    }

    [Theory]
    [InlineData(".# 1", 1)]
    [InlineData("???.### 1,1,3", 1)]
    public void AdditionalExamples_Part2(string notes, int expected)
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(notes);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(expected);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(1_738_259_948_652L);
    }
}

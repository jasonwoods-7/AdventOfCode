namespace AoC.Tests.Y2023.Day11;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day11Tests : AoCRunnerTests<AoC.Y2023.Day11.Day11>
{
    [Fact]
    public async Task Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(1L);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(374L);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner(1L);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(9_947_476L);
    }

    [Theory]
    [InlineData(10L, 1_030L)]
    [InlineData(100L, 8_410L)]
    public async Task Example_Part2(long multiplier, long expected)
    {
        // Arrange
        var runner = CreateRunner(multiplier - 1L);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(expected);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner(999_999L);

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(519_939_907_614L);
    }
}

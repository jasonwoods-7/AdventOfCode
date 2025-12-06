namespace AoC.Tests.Y2022.Day09;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day09Tests : AoCRunnerTests<AoC.Y2022.Day09.Day09>
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
        actual.ShouldBe(13);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.ShouldBe(5_779);
    }

    [Theory]
    [InlineData("example.txt", 1)]
    [InlineData("example2.txt", 36)]
    public async Task Example_Part2(string fileName, int expected)
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync(fileName));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(expected);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.ShouldBe(2_331);
    }
}

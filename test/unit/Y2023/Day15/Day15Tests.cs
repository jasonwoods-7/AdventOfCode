using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day15;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day15Tests : AoCRunnerTests<AoC.Y2023.Day15.Day15>
{
    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(511_343);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7");

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(145);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(294_474);
    }
}

using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day15;

public class Day15Tests
{
    [SkippableFact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(511_343);
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
        result.Should().Be(145);
    }

    [SkippableFact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(294_474);
    }

    static IAoCRunner<IEnumerable<string>, int> CreateRunner() => new AoC.Y2023.Day15.Day15();
}

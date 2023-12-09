namespace AoC.Tests.Y2023.Day08;

public class Day08Tests
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
        result.Should().Be(18_023L);
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
        result.Should().Be(14_449_445_933_179L);
    }

    static IAoCRunner<AoC.Y2023.Day08.Parsed, long> CreateRunner() => new AoC.Y2023.Day08.Day08();
}

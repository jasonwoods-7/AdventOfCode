namespace AoC.Tests.Y2023.Day02;

public class Day02Tests
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
        result.Should().Be(2_176);
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
        result.Should().Be(63_700);
    }

    static IAoCRunner<IReadOnlyList<AoC.Y2023.Day02.Game>, int> CreateRunner() => new AoC.Y2023.Day02.Day02();
}

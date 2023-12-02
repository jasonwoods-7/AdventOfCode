using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day01;

public class Day01Tests
{
    [Fact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(54_953);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(
            """
            two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen
            """);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(281);
    }

    [Fact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(53_868);
    }

    static IAoCRunner<IReadOnlyList<string>, int> CreateRunner() => new AoC.Y2023.Day01.Day01();
}

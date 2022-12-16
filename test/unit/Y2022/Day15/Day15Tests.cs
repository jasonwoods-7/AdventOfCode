using AnyOfTypes;
using AoC.Y2022.Day15;

namespace AoC.Tests.Y2022.Day15;

public class Day15Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner(new Part1Data(10));

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(26);
    }

    [Fact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner(new Part1Data(2_000_000));

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(4_876_693);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner(new Part2Data(20));

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(56_000_011);
    }

    [Fact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner(new Part2Data(4_000_000));

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(11_645_454_855_041);
    }

    static IAoCRunner<IReadOnlyList<(Coord, Coord)>, long> CreateRunner(AnyOf<Part1Data, Part2Data> data) =>
        new AoC.Y2022.Day15.Day15(data, NullLogger<AoC.Y2022.Day15.Day15>.Instance);
}

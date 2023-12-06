namespace AoC.Tests.Y2023.Day05;

public class Day05Tests
{
    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(35L);
    }

    [Fact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.Should().Be(323_142_486L);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(46L);
    }

    [Fact(Skip = "Not optimized")]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.Should().Be(79_874_951L);
    }

    static IAoCRunner<AoC.Y2023.Day05.Parsed, long> CreateRunner() => new AoC.Y2023.Day05.Day05();
}

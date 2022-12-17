namespace AoC.Tests.Y2022.Day17;

public class Day17Tests
{
    readonly ILoggerFactory _loggerFactory;

    public Day17Tests(ITestOutputHelper outputHelper) =>
        _loggerFactory = outputHelper.CreateLoggerFactory();

    [Fact]
    public void Example_Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(3_068);
    }

    [Fact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(3_163);
    }

    [Fact]
    public void Example_Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile("example.txt"));

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(1_514_285_714_288);
    }

    [Fact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(1_560_932_944_615);
    }

    IAoCRunner<IEnumerable<Func<Coord, Coord>>, long> CreateRunner() =>
        new AoC.Y2022.Day17.Day17(_loggerFactory.CreateLogger<AoC.Y2022.Day17.Day17>());
}

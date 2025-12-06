using AoC.Tests.Extensions;

namespace AoC.Tests.Y2023.Day10;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
[SuppressMessage("ReSharper", "AsyncApostle.ConfigureAwaitHighlighting")]
public class Day10Tests(ITestOutputHelper outputHelper) : AoCRunnerTests<AoC.Y2023.Day10.Day10>
{
    readonly ILoggerFactory _loggerFactory = outputHelper.CreateLoggerFactory();

    [Theory]
    [InlineData(
        """
            .....
            .S-7.
            .|.|.
            .L-J.
            .....
            """,
        4
    )]
    [InlineData(
        """
            ..F7.
            .FJ|.
            SJ.L7
            |F--J
            LJ...
            """,
        8
    )]
    public void Example_Part1(string map, int expected)
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day10.Day10>());

        var input = runner.ParseInput(map);

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(expected);
    }

    public override async Task Part1()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day10.Day10>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart1(input);

        // Assert
        result.ShouldBe(6_828);
    }

    [Theory]
    [InlineData(
        """
            ...........
            .S-------7.
            .|F-----7|.
            .||.....||.
            .||.....||.
            .|L-7.F-J|.
            .|..|.|..|.
            .L--J.L--J.
            ...........
            """,
        4
    )]
    [InlineData(
        """
            ..........
            .S------7.
            .|F----7|.
            .||OOOO||.
            .||OOOO||.
            .|L-7F-J|.
            .|II||II|.
            .L--JL--J.
            ..........
            """,
        4
    )]
    [InlineData(
        """
            .F----7F7F7F7F-7....
            .|F--7||||||||FJ....
            .||.FJ||||||||L7....
            FJL7L7LJLJ||LJ.L-7..
            L--J.L7...LJS7F-7L7.
            ....F-J..F7FJ|L7L7L7
            ....L7.F7||L7|.L7L7|
            .....|FJLJ|FJ|F7|.LJ
            ....FJL-7.||.||||...
            ....L---J.LJ.LJLJ...
            """,
        8
    )]
    [InlineData(
        """
            FF7FSF7F7F7F7F7F---7
            L|LJ||||||||||||F--J
            FL-7LJLJ||||||LJL-77
            F--JF--7||LJLJ7F7FJ-
            L---JF-JLJ.||-FJLJJ7
            |F|F-JF---7F7-L7L|7|
            |FFJF7L7F-JF7|JL---7
            7-L-JL7||F7|L7F-7F7|
            L.L7LFJ|||||FJL7||LJ
            L7JLJL-JLJLJL--JLJ.L
            """,
        10
    )]
    public void Example_Part2(string map, int expected)
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day10.Day10>());

        var input = runner.ParseInput(map);

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(expected);
    }

    public override async Task Part2()
    {
        // Arrange
        var runner = CreateRunner(_loggerFactory.CreateLogger<AoC.Y2023.Day10.Day10>());

        var input = runner.ParseInput(await InputHelpers.ReadInputFileAsync());

        // Act
        var result = runner.RunPart2(input);

        // Assert
        result.ShouldBe(459);
    }
}

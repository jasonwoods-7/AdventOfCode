namespace AoC.Tests.Y2022.Day06;

public class Day06Tests
{
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void Example_Part1(string input, int expected)
    {
        // Arrange
        var runner = CreateRunner();

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(expected);
    }

    [SkippableFact]
    public void Part1()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart1(input);

        // Assert
        actual.Should().Be(1_623);
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void Example_Part2(string input, int expected)
    {
        // Arrange
        var runner = CreateRunner();

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(expected);
    }

    [SkippableFact]
    public void Part2()
    {
        // Arrange
        var runner = CreateRunner();

        var input = runner.ParseInput(InputHelpers.ReadInputFile());

        // Act
        var actual = runner.RunPart2(input);

        // Assert
        actual.Should().Be(3_774);
    }

    static IAoCRunner<string, int> CreateRunner() => new AoC.Y2022.Day06.Day06();
}

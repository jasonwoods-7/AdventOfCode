using AoC.Helpers;

namespace AoC.Tests.Helpers;

public class NumberHelperTests
{
    [Theory]
    [InlineData(4, 9, 1)]
    [InlineData(40, 6, 2)]
    public void GreatestCommonDivisor(long num1, long num2, long expected)
    {
        // Arrange

        // Act
        var actual = NumberHelpers.GreatestCommonDivisor(num1, num2);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(4, 9, 36)]
    [InlineData(4, 18, 36)]
    public void LeastCommonMultiple(long num1, long num2, long expected)
    {
        // Arrange

        // Act
        var actual = NumberHelpers.LeastCommonMultiple(num1, num2);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(4, 9, true)]
    [InlineData(40, 6, false)]
    public void Coprime(long num1, long num2, bool expected)
    {
        // Arrange

        // Act
        var actual = NumberHelpers.Coprime(num1, num2);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(42, 2017, 1969)]
    [InlineData(40, 1, 0)]
    [InlineData(52, -217, 96)]
    [InlineData(-486, 217, 121)]
    [InlineData(40, 2018, -1)]
    public void ModInverse(long num, long mod, long expected)
    {
        // Arrange

        // Act
        var actual = NumberHelpers.ModInverse(num, mod);

        // Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new long[] { 3, 5, 7 }, new long[] { 2, 3, 2 }, 23)]
    public void ChineseRemainderTheorem(long[] n, long[] a, long expected)
    {
        // Arrange

        // Act
        var actual = NumberHelpers.ChineseRemainderTheorem(n, a);

        // Assert
        actual.Should().Be(expected);
    }
}

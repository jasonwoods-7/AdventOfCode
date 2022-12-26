namespace AoC.Y2022.Day25;

public class Day25 : IAoCRunner<IEnumerable<long>, string>
{
    public IEnumerable<long> ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Select(FromSnafu);

    public string RunPart1(IEnumerable<long> input) => input
        .Sum()
        .Apply(ToSnafu);

    public string RunPart2(IEnumerable<long> input) => string.Empty;

    static long FromSnafu(string snafu) => snafu
        .Reverse()
        .Index()
        .Sum(t => (long)Math.Pow(5, t.index) * t.item switch
        {
            '=' => -2,
            '-' => -1,
            _ => t.item - '0'
        });

    static string ToSnafu(long number)
    {
        var snafuNum = "";

        while (number > 0)
        {
            number = Math.DivRem(number, 5, out var remainder);
            (snafuNum, number) = remainder switch
            {
                3 => ("=" + snafuNum, number + 1),
                4 => ("-" + snafuNum, number + 1),
                _ => (remainder.ToString(CultureInfo.CurrentCulture) + snafuNum, number)
            };
        }

        return snafuNum;
    }
}

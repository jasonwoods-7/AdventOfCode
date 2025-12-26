using CommunityToolkit.HighPerformance;

namespace AoC.Y2025.Day06;

public class Day06 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.Partition(
            l => !l.ContainsAny(['+', '*']),
            (digits, operators) =>
            {
                var digs = digits.ToArray();
                var ops = operators.Single().Where(o => o != ' ').ToImmutableList();

                return new ParsedInput(
                    new ReadOnlyMemory2D<char>(
                        digs.SelectMany(l => l).ToArray(),
                        digs.Length,
                        digs[0].Length
                    ),
                    ops
                );
            }
        );

    public long RunPart1(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        return Solver(input, NumberParser);

        static long[,] NumberParser(ReadOnlyMemory2D<char> digits, int opCount)
        {
            var result = new long[opCount, digits.Height];

            for (var row = 0; row < digits.Height; ++row)
            {
                var nums = new string(digits.Span.GetRowSpan(row))
                    .FindNumbers<long>()
                    .ToImmutableList();

                for (var col = 0; col < nums.Count; ++col)
                {
                    result[col, row] = nums[col];
                }
            }

            return result;
        }
    }

    public long RunPart2(
        ParsedInput input,
        object? state = null,
        CancellationToken cancellationToken = default
    )
    {
        return Solver(input, NumberParser);

        static long[,] NumberParser(ReadOnlyMemory2D<char> digits, int opCount)
        {
            var result = new long[opCount, digits.Height];

            var current = ImmutableList.CreateBuilder<long>();
            var row = 0;

            for (var column = 0; column < digits.Width; ++column)
            {
                var value = new string(digits.Span.GetColumn(column).ToArray()).Trim();

                if (value.Length == 0)
                {
                    for (var counter = 0; counter < current.Count; ++counter)
                    {
                        result[row, counter] = current[counter];
                    }

                    current.Clear();
                    row++;
                }
                else
                {
                    current.Add(value.ParseNumber<long>());
                }
            }

            for (var counter = 0; counter < current.Count; ++counter)
            {
                result[row, counter] = current[counter];
            }

            return result;
        }
    }

    static long Solver(ParsedInput input, Func<ReadOnlyMemory2D<char>, int, long[,]> numberParser)
    {
        var result = 0L;

        var opCount = input.Operators.Count;

        var numbers = numberParser(input.Digits, opCount);

        for (var y = 0; y < opCount; ++y)
        {
            var aggregator = GetAggregator(input.Operators[y]);
            var value = numbers[y, 0];

            for (var x = 1; x < numbers.GetLength(1); ++x)
            {
                var num = numbers[y, x];

                if (num != 0)
                {
                    value = aggregator(value, num);
                }
            }

            result += value;
        }

        return result;

        static Func<long, long, long> GetAggregator(char op) =>
            op == '+' ? (a, b) => a + b : (a, b) => a * b;
    }
}

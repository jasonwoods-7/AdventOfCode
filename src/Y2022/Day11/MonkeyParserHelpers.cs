using System.Linq.Expressions;

namespace AoC.Y2022.Day11;

public static class MonkeyParserHelpers
{
    public static Func<long, long> CreateOperationFunc(string operation)
    {
        Debug.Assert(operation.StartsWith("  Operation: new = old ", StringComparison.Ordinal));

        var oldParam = Expression.Parameter(typeof(long), "old");
        var value = operation.FindNumbers<long>().HeadOrNone();
        var rightOperand = value.IsSome
            ? (Expression)Expression.Constant(value.Single())
            : oldParam;
        var binaryExpression =
            operation[23] == '*'
                ? Expression.Multiply(oldParam, rightOperand)
                : Expression.Add(oldParam, rightOperand);
        var operationLambda = Expression.Lambda<Func<long, long>>(binaryExpression, oldParam);
        return operationLambda.Compile();
    }

    public static Func<long, int> CreateTestFunc(long testDiv, int trueMonkey, int falseMonkey)
    {
        var divRemInfo = typeof(Math).GetMethod(nameof(Math.DivRem), [typeof(long), typeof(long)])!;

        var valueParam = Expression.Parameter(typeof(long), "value");
        var constTest = Expression.Constant(testDiv);
        var divRemCall = Expression.Call(divRemInfo, valueParam, constTest);
        var item2Field = Expression.Field(divRemCall, nameof(ValueTuple<long, long>.Item2));
        var zeroValue = Expression.Constant(0L);
        var equalTest = Expression.Equal(item2Field, zeroValue);
        var result = Expression.Condition(
            equalTest,
            Expression.Constant(trueMonkey),
            Expression.Constant(falseMonkey),
            typeof(int)
        );
        var testLambda = Expression.Lambda<Func<long, int>>(result, valueParam);
        return testLambda.Compile();
    }
}

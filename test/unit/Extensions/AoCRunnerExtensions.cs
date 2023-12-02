namespace AoC.Tests.Extensions;

static class AoCRunnerExtensions
{
    public static TInput ParseInput<TInput, TResult>(
        this IAoCRunner<TInput, TResult> source,
        string input) =>
        source.ParseInput(Lines(input));

    static IEnumerable<string> Lines(string input)
    {
        using var reader = new StringReader(input);

        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}

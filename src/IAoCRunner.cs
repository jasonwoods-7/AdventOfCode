namespace AoC;

public interface IAoCRunner<TInput, out TResult>
{
    TInput ParseInput(string[] puzzleInput);

    TResult RunPart1(TInput input);

    TResult RunPart2(TInput input);
}

namespace AoC;

public interface IAoCRunner<TInput, out TResult>
{
    TInput ParseInput(IEnumerable<string> puzzleInput);

    TResult RunPart1(
        TInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    );

    TResult RunPart2(
        TInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    );
}

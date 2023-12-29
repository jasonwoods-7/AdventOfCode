namespace AoC.Tests;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public abstract class AoCRunnerTests<TRunner>
{
#pragma warning disable xUnit1013
    [SkippableFact]
    public abstract Task Part1();

    [SkippableFact]
    public abstract Task Part2();
#pragma warning restore

    protected TRunner CreateRunner(params object[] args) =>
        (TRunner)Activator.CreateInstance(typeof(TRunner), args)!;
}

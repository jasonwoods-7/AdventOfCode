namespace AoC.Tests;

[SuppressMessage("ReSharper", "AsyncApostle.AsyncMethodNamingHighlighting")]
public abstract class AoCRunnerTests<TRunner>
{
    [Fact]
    public abstract Task Part1();

    [Fact]
    public abstract Task Part2();

    protected TRunner CreateRunner(params object[] args) =>
        (TRunner)Activator.CreateInstance(typeof(TRunner), args)!;
}

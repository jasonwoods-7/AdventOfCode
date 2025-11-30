namespace AoC.Tests.Helpers;

public static class OutputHelpers
{
    public static ILoggerFactory CreateLoggerFactory(this ITestOutputHelper outputHelper) =>
        new LoggerFactory().AddSerilog(
            new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(outputHelper, formatProvider: null)
                .CreateLogger()
        );
}

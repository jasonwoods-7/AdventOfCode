using Serilog.Sinks.XUnit3;

namespace AoC.Tests.Helpers;

public static class OutputHelpers
{
    public static ILoggerFactory CreateLoggerFactory(this ITestOutputHelper outputHelper) =>
        new LoggerFactory().AddSerilog(
            new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.XUnit3TestOutput()
                .CreateLogger()
        );
}

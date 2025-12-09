using System.Diagnostics;

namespace AoC.Tests.Helpers;

static class TokenSourceHelpers
{
    public static CancellationTokenSource CreateDefaultTokenSource(int waitSeconds = 15)
    {
        var cts = CancellationTokenSource.CreateLinkedTokenSource(
            TestContext.Current.CancellationToken
        );

        if (!Debugger.IsAttached)
        {
            cts.CancelAfter(TimeSpan.FromSeconds(waitSeconds));
        }

        return cts;
    }
}

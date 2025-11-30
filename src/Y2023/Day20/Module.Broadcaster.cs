namespace AoC.Y2023.Day20;

#pragma warning disable CA1716
partial record Module
#pragma warning restore CA1716
{
    partial record Broadcaster
    {
        protected override IEnumerable<(string, string, Pulse)> SendInternal(
            string sender,
            Pulse pulse,
            long buttonPress
        ) => DestinationModules.Select(m => (Name, m, pulse));
    }
}

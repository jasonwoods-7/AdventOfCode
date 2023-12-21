namespace AoC.Y2023.Day20;

partial record Module
{
    partial record Broadcaster
    {
        protected override IEnumerable<(string, string, Pulse)> SendInternal(string sender, Pulse pulse, long buttonPress) =>
            DestinationModules
                .Select(m => (Name, m, pulse));
    }
}

namespace AoC.Y2023.Day20;

partial record Module
{
    partial record Untyped
    {
        public override ImmutableArray<string> DestinationModules { get; init; } = [];

        protected override IEnumerable<(string, string, Pulse)> SendInternal(
            string sender,
            Pulse pulse,
            long buttonPress
        ) => [];
    }
}

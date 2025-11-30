namespace AoC.Y2023.Day20;

partial record Module
{
    partial record FlipFlop
    {
        bool On { get; set; }

        protected override IEnumerable<(string, string, Pulse)> SendInternal(
            string sender,
            Pulse pulse,
            long buttonPress
        )
        {
            if (pulse == Pulse.High)
            {
                return System.Array.Empty<(string, string, Pulse)>();
            }

            On = !On;

            var nextPulse = On ? Pulse.High : Pulse.Low;

            return DestinationModules.Select(m => (Name, m, nextPulse));
        }
    }
}

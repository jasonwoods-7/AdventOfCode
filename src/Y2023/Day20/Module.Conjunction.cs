namespace AoC.Y2023.Day20;

partial record Module
{
    partial record Conjunction
    {
        readonly Dictionary<string, Pulse> _lastPulses = new(StringComparer.Ordinal);

        public void AddInput(string input) => _lastPulses.Add(input, Pulse.Low);

        public IEnumerable<string> GetInputs() => _lastPulses.Keys;

        public event EventHandler<ConjunctionEventArgs>? AllHigh;

        protected override IEnumerable<(string, string, Pulse)> SendInternal(
            string sender,
            Pulse pulse,
            long buttonPress
        )
        {
            Debug.Assert(_lastPulses.ContainsKey(sender));

            _lastPulses[sender] = pulse;

            var nextPulse = _lastPulses.All(p => p.Value == Pulse.High) ? Pulse.Low : Pulse.High;

            if (nextPulse == Pulse.High && AllHigh is not null)
            {
                AllHigh(this, new ConjunctionEventArgs(buttonPress));
            }

            return DestinationModules.Select(m => (Name, m, nextPulse));
        }
    }
}

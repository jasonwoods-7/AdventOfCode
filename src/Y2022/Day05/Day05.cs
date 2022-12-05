using static MoreLinq.Extensions.ChooseExtension;
using static MoreLinq.Extensions.FoldExtension;
using static MoreLinq.Extensions.SplitExtension;
using Cargo = System.Collections.Immutable.ImmutableDictionary<int, System.Collections.Immutable.ImmutableStack<char>>;

namespace AoC.Y2022.Day05;

public class Day05 : IAoCRunner<(Cargo, (int, int, int)[]), string>
{
    public (Cargo, (int, int, int)[]) ParseInput(IEnumerable<string> puzzleInput) => puzzleInput
        .Split(static l => l == string.Empty)
        .Fold(static (cargo, instructions) =>
        {
            var parsedCargo = cargo
                .TakeWhile(static l => l.Contains('['))
                .SelectMany(static l => IntegerRange
                    .FromMinMax(1, l.Length, 4)
                    .Choose(i => (l[i] != ' ', (l[i], i >> 2))))
                .GroupBy(static t => t.Item2, static t => t.Item1)
                .ToImmutableDictionary(static g => g.Key + 1, static g => ImmutableStack.CreateRange(g.Reverse()));

            var parsedInstructions = instructions
                .Select(static l => l
                    .FindIntegers()
                    .Fold(static (a, b, c) => (a, b, c)))
                .ToArray();

            return (parsedCargo, parsedInstructions);
        });

    public string RunPart1((Cargo, (int, int, int)[]) input) => MoveCrates(
        input.Item1,
        static (instructions, cargo) =>
        {
            var (count, source, destination) = instructions;

            var nextCargo = cargo;

            for (var counter = 0; counter < count; ++counter)
            {
                nextCargo = nextCargo.SetItem(source, nextCargo[source].Pop(out var value));
                nextCargo = nextCargo.SetItem(destination, nextCargo[destination].Push(value));
            }

            return nextCargo;
        },
        input.Item2);

    public string RunPart2((Cargo, (int, int, int)[]) input) => MoveCrates(
        input.Item1,
        static (instructions, cargo) =>
        {
            var (count, source, destination) = instructions;

            var nextCargo = cargo;

            var sourceCrates = new Stack<char>();

            for (var counter = 0; counter < count; ++counter)
            {
                nextCargo = nextCargo.SetItem(source, nextCargo[source].Pop(out var value));
                sourceCrates.Push(value);
            }

            for (var counter = 0; counter < count; ++counter)
            {
                nextCargo = nextCargo.SetItem(destination, nextCargo[destination].Push(sourceCrates.Pop()));
            }

            return nextCargo;
        },
        input.Item2);

    static string MoveCrates(
        Cargo cargo,
        Func<(int, int, int), Cargo, Cargo> calculateMoves,
        Span<(int, int, int)> instructions)
    {
        if (instructions.Length == 0)
        {
            return cargo
                .OrderBy(static s => s.Key)
                .Aggregate("", static (s, c) => s + c.Value.Peek());
        }

        return MoveCrates(
            calculateMoves(instructions[0], cargo),
            calculateMoves,
            instructions[1..]);
    }
}

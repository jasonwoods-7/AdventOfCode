using Cargo = System.Collections.Immutable.ImmutableDictionary<
    int,
    System.Collections.Immutable.ImmutableStack<char>
>;
using Instruction = System.ValueTuple<int, int, int>;

namespace AoC.Y2022.Day05;

public class Day05 : IAoCRunner<(Cargo, Instruction[]), string>
{
    public (Cargo, Instruction[]) ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput
            .Split(static l => l == string.Empty)
            .Fold(
                static (cargo, instructions) =>
                {
                    var parsedCargo = cargo
                        .TakeWhile(static l => l.Contains('['))
                        .SelectMany(static l =>
                            IntegerRange
                                .FromMinMax(1, l.Length, 4)
                                .Choose(i => (l[i] != ' ', (l[i], i >> 2)))
                        )
                        .GroupBy(static t => t.Item2, static t => t.Item1)
                        .ToImmutableDictionary(
                            static g => g.Key + 1,
                            static g => ImmutableStack.CreateRange(g.Reverse())
                        );

                    var parsedInstructions = instructions
                        .Select(static l =>
                            l.FindNumbers<int>().Fold(static (a, b, c) => (a, b, c))
                        )
                        .ToArray();

                    return (parsedCargo, parsedInstructions);
                }
            );

    public string RunPart1(
        (Cargo, (int, int, int)[]) input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        MoveCrates(
            input,
            static (cargo, instruction) =>
            {
                var (count, source, destination) = instruction;

                var nextCargo = cargo;

                for (var counter = 0; counter < count; ++counter)
                {
                    nextCargo = nextCargo.SetItem(source, nextCargo[source].Pop(out var value));
                    nextCargo = nextCargo.SetItem(destination, nextCargo[destination].Push(value));
                }

                return nextCargo;
            }
        );

    public string RunPart2(
        (Cargo, (int, int, int)[]) input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    ) =>
        MoveCrates(
            input,
            static (cargo, instruction) =>
            {
                var (count, source, destination) = instruction;

                var nextCargo = cargo;

                var sourceCrates = new Stack<char>();

                for (var counter = 0; counter < count; ++counter)
                {
                    nextCargo = nextCargo.SetItem(source, nextCargo[source].Pop(out var value));
                    sourceCrates.Push(value);
                }

                for (var counter = 0; counter < count; ++counter)
                {
                    nextCargo = nextCargo.SetItem(
                        destination,
                        nextCargo[destination].Push(sourceCrates.Pop())
                    );
                }

                return nextCargo;
            }
        );

    static string MoveCrates(
        (Cargo cargo, Instruction[] instructions) input,
        Func<Cargo, Instruction, Cargo> calculateMoves
    ) =>
        input.instructions.Aggregate(
            input.cargo,
            calculateMoves,
            static c =>
                c.OrderBy(static c => c.Key).Aggregate("", static (s, c) => s + c.Value.Peek())
        );
}

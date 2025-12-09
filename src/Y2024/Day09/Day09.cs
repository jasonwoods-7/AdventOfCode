using ParsedInput = System.Collections.Generic.IReadOnlyList<int>;

namespace AoC.Y2024.Day09;

public class Day09 : IAoCRunner<ParsedInput, long>
{
    public ParsedInput ParseInput(IEnumerable<string> puzzleInput) =>
        puzzleInput.SelectMany(l => l, (_, c) => c - '0').ToList();

    public long RunPart1(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var memory = input
            .Index()
            .SelectMany(t => Enumerable.Repeat((t.Index & 1) == 0 ? t.Index >> 1 : -1, t.Item))
            .ToList();

        var end = memory.Count - 1;
        var start = 0;

        while (end > start)
        {
            while (memory[end] == -1)
            {
                end--;
            }

            var current = memory[end];
            memory[end] = -1;

            while (memory[start] != -1)
            {
                start++;
            }

            memory[start] = current;
        }

        var sum = 0L;

        for (var index = 0; memory[index] != -1; ++index)
        {
            sum += index * memory[index];
        }

        return sum;
    }

    public long RunPart2(
        ParsedInput input,
        object[]? additionalParams = null,
        CancellationToken cancellationToken = default
    )
    {
        var memory = input
            .Index()
            .SelectMany(t => Enumerable.Repeat((t.Index & 1) == 0 ? t.Index >> 1 : -1, t.Item))
            .ToList();

        var end = memory.Count - 1;
        var start = 0;

        while (end > start)
        {
            var savedStart = memory.IndexOf(-1);

            while (memory[end] == -1)
            {
                end--;
            }

            var file = memory[end];
            var fileEnd = end--;
            while (memory[end] == file)
            {
                end--;
            }

            var fileStart = end + 1;
            var fileLength = fileEnd - fileStart;

            var moved = false;

            while (end > start && !moved)
            {
                while (memory[start] != -1)
                {
                    start++;
                }

                var freeStart = start;
                while (start < memory.Count && memory[start] == -1)
                {
                    start++;
                }

                var freeEnd = --start;
                var freeLength = freeEnd - freeStart;

                if (freeLength >= fileLength && fileStart > freeStart)
                {
                    for (var counter = 0; counter <= fileLength; ++counter)
                    {
                        memory[freeStart + counter] = file;
                        memory[fileStart + counter] = -1;
                    }

                    moved = true;
                }
                else
                {
                    start = freeEnd + 1;
                }
            }

            start = savedStart;
        }

        var sum = 0L;

        for (var index = 0; index < memory.Count; ++index)
        {
            if (memory[index] != -1)
            {
                sum += index * memory[index];
            }
        }

        return sum;
    }
}

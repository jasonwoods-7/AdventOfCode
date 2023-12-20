using System.Text;

namespace AoC.Y2023.Day19;

static class InstructionParser
{
    public static Instruction ParseInstruction(string instruction) =>
        Tokenize(instruction)
            .Aggregate((IState)new InstructionState(),
                (s, t) => t.Process(s))
            .ToInstruction();

    static IEnumerable<IToken> Tokenize(string instruction)
    {
        var currentToken = new StringBuilder();

        foreach (var current in instruction)
        {
            switch (current)
            {
                case '<':
                case '>':
                    yield return InstructionNameToken();
                    yield return new InequalityToken(current);
                    break;
                case ':':
                    yield return InstructionNameToken();
                    yield return new TrueToken();
                    break;
                case ',':
                    yield return InstructionNameToken();
                    yield return new FalseToken();
                    break;
                default:
                    currentToken.Append(current);
                    break;
            }
        }

        yield return InstructionNameToken();
        yield return new EndToken();

        IToken InstructionNameToken()
        {
            if (currentToken.Length == 0)
            {
                return new NullToken();
            }

            var token = currentToken.ToString();
            currentToken.Clear();
            return new InstructionNameToken(token);
        }
    }
}

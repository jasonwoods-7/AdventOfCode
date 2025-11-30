namespace AoC.Y2023.Day19;

sealed record HyperCube(Range X, Range M, Range A, Range S)
{
    public (HyperCube, HyperCube) SplitCube(int splitAt, string dimension) =>
        dimension switch
        {
            "X" => (
                this with
                {
                    X = new Range(X.Start, splitAt),
                },
                this with
                {
                    X = new Range(splitAt + 1, X.End),
                }
            ),
            "M" => (
                this with
                {
                    M = new Range(M.Start, splitAt),
                },
                this with
                {
                    M = new Range(splitAt + 1, M.End),
                }
            ),
            "A" => (
                this with
                {
                    A = new Range(A.Start, splitAt),
                },
                this with
                {
                    A = new Range(splitAt + 1, A.End),
                }
            ),
            "S" => (
                this with
                {
                    S = new Range(S.Start, splitAt),
                },
                this with
                {
                    S = new Range(splitAt + 1, S.End),
                }
            ),
            _ => throw new InvalidOperationException(),
        };

    public long CalculateArea() =>
        1L
        * (X.End.Value - X.Start.Value + 1)
        * (M.End.Value - M.Start.Value + 1)
        * (A.End.Value - A.Start.Value + 1)
        * (S.End.Value - S.Start.Value + 1);
}

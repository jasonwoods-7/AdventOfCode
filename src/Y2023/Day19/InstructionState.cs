using System.Linq.Expressions;

namespace AoC.Y2023.Day19;

sealed class InstructionState(InstructionState? parent = null) : IState
{
    string? _name;
    char? _inequality;
    int? _value;
    InstructionState? _trueInstruction;
    InstructionState? _falseInstruction;

    public IState OnReadToken(string name)
    {
        _name = name;
        return this;
    }

    public IState OnInequalityToken(char inequality)
    {
        _inequality = inequality;
        return new ValueState(this);
    }

    public IState OnTrueToken()
    {
        if (_trueInstruction is not null ||
            !_inequality.HasValue)
        {
            return parent!.OnTrueToken();
        }

        _trueInstruction = new InstructionState(this);
        return _trueInstruction;
    }

    public IState OnFalseToken()
    {
        if (_falseInstruction is not null ||
            !_inequality.HasValue)
        {
            return parent!.OnFalseToken();
        }

        _falseInstruction = new InstructionState(this);
        return _falseInstruction;
    }

    public IState OnEndToken() =>
        parent?.OnEndToken() ?? this;

    public Instruction ToInstruction()
    {
        Debug.Assert(_name is not null);

        if (!_inequality.HasValue)
        {
            return new Instruction.Workflow(_name);
        }

        Debug.Assert(_value.HasValue);
        Debug.Assert(_trueInstruction is not null);
        Debug.Assert(_falseInstruction is not null);

        var param = Expression.Parameter(typeof(Part), "p");
        var access = Expression.Property(param, _name.ToUpperInvariant());
        var constant = Expression.Constant(_value);
        var check = _inequality == '<'
            ? Expression.LessThan(access, constant)
            : Expression.GreaterThan(access, constant);
        var lambda = Expression.Lambda<Func<Part, bool>>(check, param);

        return new Instruction.Rule(
            lambda,
            lambda.Compile(),
            _trueInstruction.ToInstruction(),
            _falseInstruction.ToInstruction());
    }

    public void SetValue(int value) => _value = value;
}

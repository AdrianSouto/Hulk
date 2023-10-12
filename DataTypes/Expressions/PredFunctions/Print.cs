using Hulk.Model;

namespace Hulk.DataTypes.Expressions.PredFunctions;

class Print : PredFunction
{
    public Print(List<MyExpression> args) : base("Print", args)
    {
    }

    protected override int CantArgs => 1;

    public override string Evaluate()
    {
        return Args[0].Evaluate();
    }

    public override string ToString()
    {
        string str = value + "(";
        foreach (MyExpression e in Args)
        {
            str += e + ", ";
        }

        return str.Substring(0, str.Length - 2) + ")";
    }
}
namespace Hulk.Model.PredFunctions;

class Sqrt : PredFunction
{
    public Sqrt(List<MyExpression> args) : base("Sqrt", args)
    {
    }

    protected override int CantArgs => 1;

    public override string Evaluate()
    {
        return Math.Sqrt(double.Parse(Args[0].Evaluate())).ToString();
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
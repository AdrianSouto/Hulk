namespace Hulk.Model.PredFunctions;

class Cot : PredFunction
{
    public Cot(List<MyExpression> args) : base("Cot", args)
    {
    }

    protected override int CantArgs => 1;

    public override string Evaluate()
    {
        return (1/Math.Tan(double.Parse(Args[0].Evaluate()))).ToString();
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
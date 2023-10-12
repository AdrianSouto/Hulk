namespace Hulk.Model.PredFunctions;

class Log : PredFunction
{ 
    public Log(List<MyExpression> args) : base("log", args)
    {
    }

    protected override int CantArgs => 2;

    public override string Evaluate()
    {
            return Math.Log(double.Parse(Args[1].Evaluate()), double.Parse(Args[0].Evaluate())).ToString();
    }

    public override string ToString()
    {
        string str = value + "(";
        foreach (MyExpression e in Args)
        {
            str += e+", ";
        }

        return str.Substring(0, str.Length - 2) + ")";
    }
}
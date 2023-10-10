namespace Hulk.Model.PredFunctions;

class Sqrt : PredFunction
{
    private MyExpression number;
    public Sqrt(MyExpression param) : base("sqrt", param)
    {
    }

    public override string Evaluate()
    {
        return Math.Sqrt(double.Parse(Param.Evaluate())).ToString();
    }
}
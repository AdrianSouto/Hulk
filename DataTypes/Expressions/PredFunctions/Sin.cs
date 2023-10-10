using Hulk.Model;

class Sin : PredFunction
{
    public Sin(MyExpression param) : base("sin", param)
    {
    }

    public override string Evaluate()
    {
        return Math.Sin(double.Parse(Param.Evaluate())).ToString();
    }
}
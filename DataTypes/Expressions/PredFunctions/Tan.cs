namespace Hulk.Model.PredFunctions;

class Tan : PredFunction
{
    public Tan(MyExpression param) : base("tan", param)
    {
    }

    public override string Evaluate()
    {
        return Math.Tan(double.Parse(Param.Evaluate())).ToString();
    }
}
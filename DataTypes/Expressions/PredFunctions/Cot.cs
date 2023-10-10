namespace Hulk.Model.PredFunctions;

class Cot : PredFunction
{
    public Cot(MyExpression param) : base("cot", param)
    {
    }

    public override string Evaluate()
    {
        return (1/Math.Tan(double.Parse(Param.Evaluate()))).ToString();
    }
}
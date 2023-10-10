namespace Hulk.Model.PredFunctions;

class Log : PredFunction
{
    public Log(MyExpression param) : base("log", param)
    {
    }

    public override string Evaluate()
    {
        return Math.Log(double.Parse(Param.Evaluate())).ToString();
    }
}
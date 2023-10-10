using Hulk.Model;

class Cos : PredFunction
{
    public Cos(MyExpression param) : base("cos", param)
    {
    }

    public override string Evaluate()
    {
        return Math.Cos(double.Parse(Param.Evaluate())).ToString();
    }
    
    
}
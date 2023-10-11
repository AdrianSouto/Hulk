using Hulk.Model;

namespace Hulk.DataTypes.Expressions.PredFunctions;

class Print : PredFunction
{
    public Print(MyExpression param) : base("print", param)
    {
    }

    public override string Evaluate()
    {
        return Param.Evaluate();
    }
}
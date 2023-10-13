using System.Linq.Expressions;

namespace Hulk.Model;

public class NegativeNumber : UnaryExpression
{
    public NegativeNumber(string value) : base(value){}
    public override string Evaluate()
    {
        return value;
    }

}
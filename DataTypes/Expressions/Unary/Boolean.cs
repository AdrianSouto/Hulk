namespace Hulk.Model;

public class Bool : UnaryExpression
{
    public Bool(string value) : base(value){}
    public override string Evaluate()
    {
        return value;
    }
    
}
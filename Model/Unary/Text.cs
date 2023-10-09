namespace Hulk.Model;

public class Text : UnaryExpression<string>
{
    public Text(string value) : base(value){}
    public override string Evaluate()
    {
        return value;
    }
}
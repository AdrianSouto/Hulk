namespace Hulk.Model;

public class Number : UnaryExpression<double>
{
    public Number(string value) : base(value){}
    public override double Evaluate()
    {
        return double.Parse(value);
    }
}
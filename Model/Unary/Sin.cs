
using Hulk.Model;

class Sin : UnaryExpression<double>
{
    private MyExpression<double> number;
    public Sin(string value, MyExpression<double> number) : base(value)
    {
        this.number = number;
    }

    public override double Evaluate()
    {
        return Math.Sin(number.Evaluate());
    }
}
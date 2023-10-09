using Hulk.Model;

class Power : BinaryMyExpression<double>
{
    public Power(MyExpression<double> left, MyExpression<double> right)
        : base(left, right)
    {
    }

    public override string value => "^";


    public override double Evaluate()
    {
        return Math.Pow(LeftMyExpression.Evaluate(), RightMyExpression.Evaluate());
    }
}
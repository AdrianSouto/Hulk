using Hulk.Model;

class Power : BinaryMyExpression
{
    public Power(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "^";


    public override string Evaluate()
    {
        return Math.Pow(double.Parse(LeftMyExpression.Evaluate()), double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
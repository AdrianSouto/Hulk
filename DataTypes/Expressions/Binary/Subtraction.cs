using Hulk.Model;

class Subtraction : BinaryMyExpression
{
    public Subtraction(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "-";

    public override string Evaluate()
    {
        return (double.Parse(LeftMyExpression.Evaluate()) - double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}

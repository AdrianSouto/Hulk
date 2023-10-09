using Hulk.Model;

class Subtraction : BinaryMyExpression<double>
{
    public Subtraction(MyExpression<double> left, MyExpression<double> right)
        : base(left, right)
    {
    }

    public override string value => "-";

    public override double Evaluate()
    {
        return LeftMyExpression.Evaluate() - RightMyExpression.Evaluate();
    }
}

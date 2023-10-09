namespace Hulk.Model;

class Addition : BinaryMyExpression<double>
{
    
    public Addition(MyExpression<double> left, MyExpression<double> right)
        : base(left, right)
    {
    }

    public override string value => "+";

    public override double Evaluate()
    {
        return LeftMyExpression.Evaluate() + RightMyExpression.Evaluate();
    }
}
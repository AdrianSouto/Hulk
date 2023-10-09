using Hulk.Model;

class Product : BinaryMyExpression<double>
{
    public Product(MyExpression<double> left, MyExpression<double> right)
        : base(left, right)
    {
    }

    public override string value => "*";

    public override double Evaluate()
    {
        return LeftMyExpression.Evaluate() * RightMyExpression.Evaluate();
    }
}
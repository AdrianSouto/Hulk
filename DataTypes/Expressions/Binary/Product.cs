using Hulk.Model;

class Product : BinaryMyExpression
{
    public Product(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "*";

    public override string Evaluate()
    {
        return (double.Parse(LeftMyExpression.Evaluate()) * double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
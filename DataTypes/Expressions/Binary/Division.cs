using Hulk.Model;

class Division : BinaryMyExpression{
    public Division(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "/";

    public override string Evaluate()
    {
        return (double.Parse(LeftMyExpression.Evaluate()) / double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
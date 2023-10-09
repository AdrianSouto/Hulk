using Hulk.Model;

class Division : BinaryMyExpression<double>{
    public Division(MyExpression<double> left, MyExpression<double> right)
        : base(left, right)
    {
    }

    public override string value => "/";

    public override double Evaluate()
    {
        return LeftMyExpression.Evaluate() / RightMyExpression.Evaluate();
    }
}
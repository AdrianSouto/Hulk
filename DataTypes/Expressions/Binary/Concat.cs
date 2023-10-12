using Hulk.Model;

public class Concat : BinaryMyExpression
{
    
    public Concat(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "@";

    public override string Evaluate()
    {
            return LeftMyExpression.Evaluate() + RightMyExpression.Evaluate();
    }
}
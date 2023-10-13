namespace Hulk.Model;
class Different : BinaryMyExpression
{
    
    public Different(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "!=";

    public override string Evaluate(){
        return (LeftMyExpression.Evaluate() != RightMyExpression.Evaluate()).ToString();
    }
}
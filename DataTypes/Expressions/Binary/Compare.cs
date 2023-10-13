namespace Hulk.Model;
class Compare : BinaryMyExpression
{
    
    public Compare(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "==";

    public override string Evaluate(){
        return (LeftMyExpression.Evaluate() == RightMyExpression.Evaluate()).ToString();
    }
}
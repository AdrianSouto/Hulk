namespace Hulk.Model;

class Or : BinaryMyExpression
{
    
    public Or(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "|";

    public override string Evaluate(){
        return (bool.Parse(LeftMyExpression.Evaluate()) || bool.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
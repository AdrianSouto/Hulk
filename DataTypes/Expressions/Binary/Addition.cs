namespace Hulk.Model;

class Addition : BinaryMyExpression
{
    
    public Addition(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "+";

    public override string Evaluate()
    {
        /*if (LeftMyExpression is Text && RightMyExpression is Text)
            return LeftMyExpression.value + RightMyExpression.value;*/
        return (double.Parse(LeftMyExpression.Evaluate()) + double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
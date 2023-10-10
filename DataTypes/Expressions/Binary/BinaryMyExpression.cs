namespace Hulk.Model;

abstract class BinaryMyExpression : MyExpression
{
    // Make the properties public
    public MyExpression LeftMyExpression { get; set; }
    protected MyExpression RightMyExpression { get; set; }

    public abstract override string value{get;}

    // Make the constructor public
    protected BinaryMyExpression(MyExpression left, MyExpression right)
    {
        LeftMyExpression = left;
        RightMyExpression = right;
    }

    public override string ToString()
    {
        return LeftMyExpression + " " + value + " " + RightMyExpression;
    }

    public abstract override string Evaluate();

}
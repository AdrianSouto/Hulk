namespace Hulk.Model;

public abstract class BinaryMyExpression : MyExpression
{
    // Make the properties public
    public MyExpression LeftMyExpression { get;}
    protected MyExpression RightMyExpression { get;}

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
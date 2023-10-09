namespace Hulk.Model;

abstract class BinaryMyExpression<T> : MyExpression<T>
{
    // Make the properties public
    public MyExpression<T> LeftMyExpression { get; set; }
    protected MyExpression<T> RightMyExpression { get; set; }

    public abstract string value{get;}

    // Make the constructor public
    protected BinaryMyExpression(MyExpression<T> leftMy, MyExpression<T> rightMy)
    {
        LeftMyExpression = leftMy;
        RightMyExpression = rightMy;
    }

    public abstract T Evaluate();
}
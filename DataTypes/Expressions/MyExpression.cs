public abstract class MyExpression : Node
{
    public abstract string Evaluate();

    public abstract string value { get; }
    public abstract override string ToString();
}
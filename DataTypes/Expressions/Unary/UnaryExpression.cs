namespace Hulk.Model;

public abstract class UnaryExpression : MyExpression
{
    protected UnaryExpression(string value) => this.value = value;
    public abstract override string Evaluate();

    public override string value { get; }

    public override string ToString()
    {
        return value;
    }
}
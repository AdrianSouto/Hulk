namespace Hulk.Model;

public abstract class UnaryExpression<T> : MyExpression<T>
{
    protected UnaryExpression(string value) => this.value = value;
    public abstract T Evaluate();

    public string value { get; }
}
namespace Hulk.Model;

public abstract class PredFunction : MyExpression
{
    protected MyExpression Param;

    protected PredFunction(string value, MyExpression param)
    {
        this.value = value;
        Param = param;
    }
    public abstract override string Evaluate();

    public override string value { get; }

    public override string ToString()
    {
        return value + "(" + Param + ")";
    }
}
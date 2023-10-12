namespace Hulk.Model;

public abstract class PredFunction : MyExpression
{
    protected List<MyExpression> Args;
    protected abstract int CantArgs { get; }
    protected PredFunction(string value, List<MyExpression> args)
    {
        if(args.Count != CantArgs)
            throw new SyntaxException("Invalid Arguments at "+value+" function");
        
        this.value = value;
        Args = args;
    }
    public abstract override string Evaluate();

    public override string value { get; }
}
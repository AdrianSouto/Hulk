abstract class MyFunction : MyExpression{
    
    public override string value
    {
        get;
    }

    public MyFunction(string value) => this.value = value;

    public abstract override string Evaluate();
}
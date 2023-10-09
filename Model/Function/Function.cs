abstract class MyFunction<T> : MyExpression<T>
{
    public static string[] PredFunctions = { "sin", "cos" };
    public string value
    {
        get;
    }

    public MyFunction(string value) => this.value = value;

    public abstract T Evaluate();
}
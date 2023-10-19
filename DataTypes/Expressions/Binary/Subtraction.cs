using Hulk.Model;

class Subtraction : BinaryMyExpression
{
    public Subtraction(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "-";

    public override string Evaluate()
    {
        try
        {
            return (double.Parse(LeftMyExpression.Evaluate()) - double.Parse(RightMyExpression.Evaluate())).ToString();
        }
        catch (FormatException)
        {
            throw new  SemanticException("No se puede restar 2 Strings, el operador de concatenacion es '@'.");
        }
    }
}

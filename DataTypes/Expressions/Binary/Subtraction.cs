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
        if (LeftMyExpression is Text || RightMyExpression is Text)
            throw new  SemanticException("No se puede restar 2 Strings, el operador de concatenacion es '@'.");
        return (double.Parse(LeftMyExpression.Evaluate()) - double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}

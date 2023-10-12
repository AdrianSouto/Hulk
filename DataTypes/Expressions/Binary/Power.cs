using Hulk.Model;

class Power : BinaryMyExpression
{
    public Power(MyExpression left, MyExpression right)
        : base(left, right)
    {
    }

    public override string value => "^";


    public override string Evaluate()
    {
        if (LeftMyExpression is Text || RightMyExpression is Text)
            throw new  SemanticException("No se puede realizar la potenciacion a 2 Strings, el operador de concatenacion es '@'.");
        return Math.Pow(double.Parse(LeftMyExpression.Evaluate()), double.Parse(RightMyExpression.Evaluate())).ToString();
    }
}
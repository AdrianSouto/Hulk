namespace Hulk.DataTypes.Variables;

public class UserFunction
{
    public string Name;
    public List<MyExpression> FunParams;
    public MyExpression FunBody;

    public UserFunction(string name, List<MyExpression> funParams, MyExpression funBody)
    {
        Name = name;
        FunParams = funParams;
        FunBody = funBody;
    }
}
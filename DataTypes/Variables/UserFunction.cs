namespace Hulk.DataTypes.Variables;

public class UserFunction
{
    public string Name;
    public List<Token> FunBody;
    public List<string> ParamNames;
    

    public UserFunction(string name, List<string> paramNames, List<Token> funBody)
    {
        Name = name;
        FunBody = funBody;
        ParamNames = paramNames;
    }
}
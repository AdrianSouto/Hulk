namespace Hulk.DataTypes.Variables;

public class UserFunction
{
    public string Name;
    public List<Token> FunBody;
    public List<string> ParamNames;
    public static List<UserFunction> UserFunctions = new();

    

    public UserFunction(string name, List<string> paramNames, List<Token> funBody)
    {
        Name = name;
        FunBody = funBody;
        ParamNames = paramNames;
    }
    public static void AddFunction(UserFunction f)
    {
        UserFunction? uf = FindFunction(f.Name);
        if(uf == null)
            UserFunctions.Add(f);
        else
        {
            Console.WriteLine("Ya existe una funcion con ese nombre, si desea sobreescribirla pulse 's' de lo contrario pulse cualquier otra tecla");
            if (Console.ReadKey().KeyChar == 's')
            {
                UserFunctions.Remove(uf);
                UserFunctions.Add(f);
            }

            Console.WriteLine();
        }
            
    }
    public static UserFunction? FindFunction(string funName)
    {
        return UserFunctions.Find(x => x.Name == funName);
    }
}
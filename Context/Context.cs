using Hulk.DataTypes.Variables;
using Hulk.Model;

public class Context
{
    public List<Variable> myVars;
    private List<UserFunction> userFunctions;

    public Context()
    {
        myVars = new List<Variable>();
        userFunctions = new List<UserFunction>();
        myVars.Add(new Variable("PI", new Number(double.Pi.ToString())));
    }

    public void AddVar(Variable v)
    {
        myVars.Add(v);
    }
    
    public void AddFunction(UserFunction f)
    {
        userFunctions.Add(f);
    }

    public Variable? FindVar(string varName)
    {
        foreach (Variable v in myVars)
        {
            if (v.Name == varName)
            {
                return v;
            }
        }

        return null;
    }
    public UserFunction? FindFunction(string funName)
    {
        foreach (UserFunction f in userFunctions)
        {
            if (f.Name == funName)
            {
                return f;
            }
        }

        return null;
    }
}
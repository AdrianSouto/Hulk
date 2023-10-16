using Hulk.DataTypes.Variables;
using Hulk.Model;

public class Context
{
    public List<Variable> myVars;

    public Context()
    {
        myVars = new List<Variable>();
        myVars.Add(new Variable("PI", new Number(double.Pi.ToString())));
    }

    public void AddVar(Variable v)
    {
        if (FindVar(v.Name) == null)
            myVars.Add(v);
        else
            throw new SyntaxException("La variable "+v.Name+" ya existe en el contexto actual.");
    }

    public Variable? FindVar(string varName)
    {
        return myVars.Find(x => x.Name == varName);

    }
    
}
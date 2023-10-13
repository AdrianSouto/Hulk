using Hulk.Model;

public class Context
{
    public List<Variable> MyVars;

    public Context()
    {
        MyVars = new List<Variable>();
        MyVars.Add(new Variable("PI", new Number(double.Pi.ToString())));
    }

    public Variable? FindVar(string varName)
    {
        foreach (Variable v in MyVars)
        {
            if (v.Name == varName)
            {
                return v;
            }
        }

        return null;
    }
}
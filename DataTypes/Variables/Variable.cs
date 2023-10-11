namespace Hulk.DataTypes.Variables;

public class Variable
{
    public enum VarType
    {
        Number,
        String,
        Bool
    }
    
    public string Name;
    public string Value;
    public VarType Type;
}

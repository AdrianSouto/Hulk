public class Variable
{
    public enum VarType
    {
        Number,
        String,
        Bool
    }
    
    public string Name;
    public MyExpression VarTree;
    public VarType Type;
}

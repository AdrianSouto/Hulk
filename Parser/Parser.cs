using System.Reflection;
using Hulk.DataTypes.Variables;
public class Parser
{
    private List<Token> tokens;
    private int current;
    public string Value;
    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
        Value = CheckSyntax();
    }

    private string CheckSyntax()
    {
        if (tokens[current].type == Token.TokenType.VarDeclarationKeyWord)
        {
            Variable myVar = new Variable();
            if (tokens[++current].type == Token.TokenType.ID)
                myVar.Name = tokens[current].value;
            else
                throw new SyntaxException("VarName expected after " + tokens[current - 1]+" in let-in expression");
            if(tokens[++current].type != Token.TokenType.Igual)
                throw new SyntaxException("'=' expected after " + tokens[current - 1]+" in let-in expression");
            if (tokens[++current].type == Token.TokenType.Text)
            {
                myVar.Type = Variable.VarType.String;
                myVar.Value = tokens[current++].value;
            }
            else
            {
                List<Token> t = new List<Token>();
                while (tokens[current].type != Token.TokenType.VarInKeyWord){
                    t.Add(tokens[current++]);
                    if (current == tokens.Count) throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
                }
                MathParser mp = new MathParser(t);
                myVar.Value = mp.value;
                
            }

            if (tokens[current++].type == Token.TokenType.VarInKeyWord)
            {
                Context c = new Context();
                c.MyVars.Add(myVar);
                List<Token> t = new List<Token>();
                while (tokens[current].type != Token.TokenType.Semicolon)
                    t.Add(tokens[current++]);
                return new MathParser(t, c).value;
            }
            throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
        }

        return "";
    }

}
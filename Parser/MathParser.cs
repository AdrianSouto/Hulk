using System.Linq.Expressions;
using Hulk.DataTypes.Expressions.PredFunctions;
using Hulk.Model;
using Hulk.Model.PredFunctions;

class MathParser
{
    private int current;
    private List<Token> tokens;
    public string value;
    public MyExpression tree;
    private Context? context;
    
    public MathParser(List<Token> tokens, Context? context = null)
    {
        context ??= new Context();
        this.context = context;
        this.tokens = tokens;
        current = 0;
        
        tree = Parse();
        value = tree.Evaluate();
    }
    MyExpression Parse()
    {
        if (tokens[current].type == Token.TokenType.VarDeclarationKeyWord)
        {
            return ParseVarDeclaration();
        }

        return ParseSum();
    }

    private MyExpression ParseVarDeclaration()
    {
        
            Variable myVar = new Variable();
            if (tokens[++current].type == Token.TokenType.ID)
                myVar.Name = tokens[current].value;
            else
                throw new SyntaxException("VarName expected after " + tokens[current - 1]+" in let-in expression");
            if (tokens[++current].type != Token.TokenType.Igual)
                throw new SyntaxException("'=' expected after " + tokens[current - 1] + " in let-in expression");
            
            current++;
            List<Token> t1 = new List<Token>();
            while (tokens[current].type != Token.TokenType.VarInKeyWord){
                t1.Add(tokens[current++]);
                if (current == tokens.Count) throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
            }
            
            myVar.VarTree = new MathParser(t1, context).tree;

            if (tokens[current++].type == Token.TokenType.VarInKeyWord)
            {
                Context c = new Context();
                c.MyVars.Add(myVar);
                List<Token> t2 = new List<Token>();
                while (tokens[current].type != Token.TokenType.Semicolon)
                    t2.Add(tokens[current++]);
                return new MathParser(t2, c).tree;
            }
            throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
    }

    private MyExpression ParseSum()
    {
        MyExpression left = Concat();
        
        while(current < tokens.Count && (tokens[current].type == Token.TokenType.Sum || tokens[current].type == Token.TokenType.Resta))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = Concat();
            if(currentOp == Token.TokenType.Sum)
                left = new Addition(left, right);
            else 
                left = new Subtraction(left, right);
        }
        if(current<tokens.Count-1) 
            throw new SyntaxException("Invalid Expression '"+tokens[current].value+"' is not a valid operator");
        return left;
    }
    
    private MyExpression Concat()
    {
        MyExpression left = ParseMult();
        
        while(current < tokens.Count && tokens[current].type == Token.TokenType.Concat)
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParseMult();
            if (currentOp == Token.TokenType.Concat)
                left = new Concat(left, right);
        }
        return left;
    }

    private MyExpression ParseMult()
    {
        MyExpression left = ParsePow();
        while(current < tokens.Count && (tokens[current].type == Token.TokenType.Product || tokens[current].type == Token.TokenType.Div))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParsePow();
            if(currentOp == Token.TokenType.Product)
                left= new Product(left, right);
            else
                left = new Division(left, right);
        }

        return left;
    }
    private MyExpression ParsePow()
    {
        MyExpression left = ParseTerm();
        while(current < tokens.Count && tokens[current].type == Token.TokenType.Pow)
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParseTerm();
            if(currentOp == Token.TokenType.Pow)
                left= new Power(left, right);
        }

        return left;
    }

    private MyExpression ParseTerm()
    {
        switch (tokens[current].type)
        {
            case Token.TokenType.Number:
                return new Number(tokens[current++].value);
            case Token.TokenType.Text:
                return new Text(tokens[current++].value);
            case Token.TokenType.Sin:
                return new Sin(GetParams());
            case Token.TokenType.Cos:
                return new Cos(GetParams());
            case Token.TokenType.Tan:
                return new Tan(GetParams());
            case Token.TokenType.Cot:
                return new Cot(GetParams());
            case Token.TokenType.Log:
                return new Log(GetParams());
            case Token.TokenType.Sqrt:
                return new Sqrt(GetParams());
            case Token.TokenType.Print:
                return new Print(GetParams());
            case Token.TokenType.OpenParenthesis:
                return SolveParenthesis();
        }

        int varIndex = context.MyVars.FindIndex(x => x.Name == tokens[current++].value);
        if (varIndex != -1)
        {
            return context.MyVars[varIndex].VarTree;
        }
        throw new SyntaxException("Invalid Expression");
    }

    private List<MyExpression> GetParams()
    {
        List<MyExpression> paramExpressions = new List<MyExpression>();
        List<Token> paramTokens = new List<Token>();
        if (tokens[++current].type == Token.TokenType.OpenParenthesis)
        {
            int parentCount = 1;
            current++;
            while (parentCount != 0)
            {
                if (tokens[current].type == Token.TokenType.OpenParenthesis)
                    parentCount++;
                else if (tokens[current].type == Token.TokenType.CloseParenthesis)
                    parentCount--;
                paramTokens.Add(tokens[current++]);
                if (parentCount != 0 && tokens[current].type == Token.TokenType.Comma)
                {
                    paramExpressions.Add(new MathParser(paramTokens, context).tree);
                    paramTokens.Clear();
                    current++;
                }
            }

            paramTokens.Remove(paramTokens.Last());
            paramExpressions.Add(new MathParser(paramTokens, context).tree);
            current++;
            return paramExpressions;
        }
        throw new SyntaxException("Missing Parenthesis after function Declaration");
    }
    private MyExpression SolveParenthesis()
    {
        List<Token> paramTokens = new List<Token>();
        int parentCount = 1;
        current++;
        while (parentCount != 0)
        {
            if (tokens[current+1].type == Token.TokenType.OpenParenthesis) 
                parentCount++;
            else if (tokens[current+1].type == Token.TokenType.CloseParenthesis) 
                parentCount--;
            paramTokens.Add(tokens[current++]);
            if(tokens[current].type == Token.TokenType.Semicolon)
                break;
        }
        current++;
        return new MathParser(paramTokens, context).tree;
    }
    

    /*private void CheckSyntax(List<Token> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].type == Token.TokenType.KeyWord)
            {
                nodes.Add(new KeyWord(tokens[i].value));
                continue;
            }
        }
    }*/

    /*static void CheckSyntax(List<Token> tokens){
        for(int x = 0; x < tokens.Count; x++){
            Token t = tokens[x];
            if(t.type == Token.TokenType.Word){
                if(char.IsDigit(t.value.First())){
                    throw new LexicalException(t.value);
                }
            }
            if(t.type == Token.TokenType.KeyWord){
                if(x == tokens.Count-1 || tokens[x+1].type != Token.TokenType.Word){
                    throw new SyntaxException("Missing VarName after "+t.value);
                }
            }
        }
    }*/
}
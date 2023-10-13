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
    private Context context;
    
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

        if (tokens[current].type == Token.TokenType.If)
        {
            current++;
            return ParseIfDeclaration();
        }

        /*if (tokens[current].type == Token.TokenType.FunDeclarationKeyWord)
        {
            current++;
            return ParseFunDeclaration();
        }*/

        MyExpression e = ParseAndOr();
        if(current<tokens.Count-1) 
            throw new SyntaxException("Invalid Expression '"+tokens[current].value+"' is not a valid operator");
        return e;
    }

    /*private MyExpression ParseFunDeclaration()
    {
        string funName;
        List<MyExpression> funParams;
        MyExpression funBody;
        if (tokens[++current].type == Token.TokenType.ID)
            funName = tokens[current].value;
        else
            throw new SyntaxException("FuncName expected after " + tokens[current - 1]+" in function declaration");

        if (tokens[++current].type == Token.TokenType.OpenParenthesis)
            funParams = GetParams();
        else
            throw new SyntaxException("FuncName expected after " + tokens[current - 1]+" in function declaration");

        
        if (tokens[++current].type != Token.TokenType.Arrow)
            throw new SyntaxException("'=>' expected after " + tokens[current - 1] + " in function declaration");
            
        current++;
        List<Token> t1 = new List<Token>();
        while (tokens[current].type != Token.TokenType.Semicolon){
            t1.Add(tokens[current++]);
            if (current == tokens.Count) throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
        }
            
        funBody = new MathParser(t1, context).tree;
        
        throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
    }*/

    private MyExpression ParseIfDeclaration()
    {
        if (tokens[current].type == Token.TokenType.OpenParenthesis)
        {
            string coditionResult = SolveParenthesis().Evaluate();
            List<Token> t1 = new List<Token>();
            if (coditionResult.ToLower() == "true")
            {
                while (tokens[current].type != Token.TokenType.Else)
                {
                    t1.Add(tokens[current++]);
                    if (current == tokens.Count) throw new SyntaxException("Missing 'else' KeyWord on if-else expression");
                }

                current++;
                return new MathParser(t1, context).tree;

            }
            while (tokens[current].type != Token.TokenType.Else)
            {
                current++;
            }

            current++;
            t1.Clear();
            while (current < tokens.Count)
            {
                t1.Add(tokens[current++]);
                //if (current == tokens.Count) throw new SyntaxException("Missing ';' KeyWord on if-else expression");
            }

            return new MathParser(t1, context).tree;
        }

        throw new SyntaxException("If");

    }

    private MyExpression ParseVarDeclaration()
    {
        string varName;
        MyExpression varValue;
            if (tokens[++current].type == Token.TokenType.ID)
                varName = tokens[current].value;
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
            
            varValue = new MathParser(t1, context).tree;

            if (tokens[current++].type == Token.TokenType.VarInKeyWord)
            {
                //Context c = new Context();
                context.MyVars.Add(new Variable(varName, varValue));
                List<Token> t2 = new List<Token>();
                while (current < tokens.Count)
                    t2.Add(tokens[current++]);
                return new MathParser(t2, context).tree;
            }
            throw new SyntaxException("Missing 'in' KeyWord on let-in expression");
    }
    
    private MyExpression ParseAndOr()
    {
        MyExpression left = Compare();
        
        while(current < tokens.Count && (
                  tokens[current].type == Token.TokenType.And || 
                  tokens[current].type == Token.TokenType.Or)
             )
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = Compare();
            switch (currentOp)
            {
                case Token.TokenType.And:
                    left = new And(left, right);
                    break;
                case Token.TokenType.Or:
                    left = new Or(left, right);
                    break;
            }
        }
        return left;
    }
    
    private MyExpression Compare()
    {
        MyExpression left = ParseSum();
        
        while(current < tokens.Count && (
                  tokens[current].type == Token.TokenType.Comparar || 
                  tokens[current].type == Token.TokenType.Different ||
                  tokens[current].type == Token.TokenType.MayorQ || 
                  tokens[current].type == Token.TokenType.MenorQ
             ))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParseSum();
            switch (currentOp)
            {
                case Token.TokenType.Comparar:
                    left = new Compare(left, right);
                    break;
                case Token.TokenType.Different:
                    left = new Different(left, right);
                    break;
                case Token.TokenType.MayorQ:
                    left = new MayorQ(left, right);
                    break;
                case Token.TokenType.MenorQ:
                    left = new MenorQ(left, right);
                    break;
            }
        }
        return left;
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
        while(current < tokens.Count && (
                  tokens[current].type == Token.TokenType.Product || 
                  tokens[current].type == Token.TokenType.Div ||
                  tokens[current].type == Token.TokenType.Resto
                  ))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParsePow();
            if(currentOp == Token.TokenType.Product)
                left= new Product(left, right);
            else if(currentOp == Token.TokenType.Div)
                left = new Division(left, right);
            else
                left = new Resto(left, right);
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
            case Token.TokenType.Bool:
                return new Bool(tokens[current++].value);
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
            case Token.TokenType.Resta:
                current++;
                return new Number((-double.Parse(ParseTerm().Evaluate())).ToString());
            case Token.TokenType.Negation:
                current++;
                return new Bool((bool.Parse(ParseTerm().Evaluate())).ToString());

        }

        Variable? v = context.FindVar(tokens[current].value);
        if (v != null)
        {
            current++;
            return v.VarTree;
        }
        throw new SyntaxException("Invalid Expression '"+tokens[current].value+"'");
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
                if (parentCount == 1 && tokens[current].type == Token.TokenType.Comma)
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
}
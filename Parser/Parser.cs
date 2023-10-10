using System.Linq.Expressions;
using Hulk.Model;
using Hulk.Model.PredFunctions;

class Parser
{
    private int current;
    private List<Token> tokens;
    public string value;
    public MyExpression tree;
    
    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
        current = 0;
        
        tree = ParseExpression();
        value = tree.Evaluate();
    }
    MyExpression ParseExpression()
    {
        return ParseSum();
    }

    private MyExpression ParseSum()
    {
        MyExpression left = ParseMult();
        
        while(current < tokens.Count && (tokens[current].type == Token.TokenType.Sum || tokens[current].type == Token.TokenType.Resta))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression right = ParseMult();
            if(currentOp == Token.TokenType.Sum)
                left = new Addition(left, right);
            else 
                left = new Subtraction(left, right);
        }
        if(current<tokens.Count-1) throw new SyntaxException("Invalid Expression '"+tokens[current].value+"' is not a valid operator");
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
            case Token.TokenType.OpenParenthesis:
                current--;
                return GetParams();
        }
        
        throw new SyntaxException("Invalid Expression");
    }

    private MyExpression GetParams()
    {
        List<Token> paramTokens = new List<Token>();
        if (tokens[++current].type == Token.TokenType.OpenParenthesis)
        {
            int parentCount = 1;
            while (parentCount != 0)
            {
                current++;
                if (tokens[current].type == Token.TokenType.OpenParenthesis) parentCount++;
                else if (tokens[current].type == Token.TokenType.CloseParenthesis) parentCount--;
                paramTokens.Add(tokens[current]);
                
            }

            current++;
            return new Parser(paramTokens).tree;
        }
        throw new SyntaxException("Missing Parenthesis after function Declaration");
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
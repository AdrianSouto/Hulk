using System.Linq.Expressions;
using Hulk.Model;

class Parser
{
    private int current;
    //private List<Token> tokens;
    public double value;
    
    public Parser(List<Token> tokens)
    {
        //this.tokens = tokens;
        current = 0;
        MyExpression<double> e = ParseExpression(tokens);
        value = e.Evaluate();
    }
    

    MyExpression<double> ParseExpression(List<Token> tokens)
    {
        return ParseSum(tokens);
    }

    private MyExpression<double> ParseSum(List<Token> tokens)
    {
        MyExpression<double> left = ParseMult(tokens);
        
        while(current < tokens.Count && (tokens[current].type == Token.TokenType.Sum || tokens[current].type == Token.TokenType.Resta))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression<double> right = ParseMult(tokens);
            if(currentOp == Token.TokenType.Sum)
                left = new Addition(left, right);
            else 
                left = new Subtraction(left, right);
        }

        return left;
    }

    private MyExpression<double> ParseMult(List<Token> tokens)
    {
        MyExpression<double> left = ParseTerm(tokens);
        while(current < tokens.Count && (tokens[current].type == Token.TokenType.Mult || tokens[current].type == Token.TokenType.Div))
        {
            Token.TokenType currentOp = tokens[current].type;
            current++;
            MyExpression<double> right = ParseTerm(tokens);
            if(currentOp == Token.TokenType.Mult)
                left= new Product(left, right);
            else
                left = new Division(left, right);
        }

        return left;
    }

    private MyExpression<double> ParseTerm(List<Token> tokens)
    {
        if (tokens[current].type == Token.TokenType.Number)
            return new Number(tokens[current++].value);


        /*if (tokens[current].type == Token.TokenType.PredFunction)
        {
            switch (tokens[current].value)
            {
                case "sin":
                    return new Sin(tokens[current].value, GetParams(tokens));

            }
        }*/
        throw new SyntaxException("Invalid Expression");
    }

    /*private MyExpression<double> GetParams(List<Token> tokens)
    {
        List<Token> paramTokens = new List<Token>();
        if (tokens[++current].type == Token.TokenType.OpenPharentesis)
        {
            while (tokens[current].type != Token.TokenType.ClosePharentesis)
            {
                paramTokens.Add(tokens[current++]);
            }

            return ParseExpression(paramTokens);
        }
        throw new SyntaxException("Missing Pharentesis after function Declaration");
    }*/

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
using Hulk.DataTypes.Expressions.PredFunctions;

static class Lexer{
    public static List<Token> tokens;
    private static int igualCount = 0;
    static string[] symbols =
    {
        "+", "-", "/", "*", "(", ")", "=" ,"\"",";", "^",",","!","&","|","%", ">", "<"
    };
    public static void ApplyLexer(string input)
    {
        if (!IsBalanced(input)) throw new SyntaxException("Input no balanceado");
        tokens = GetTokens(input);
    }

    public static bool IsBalanced(string input)
    {
        if (!input.Contains('(') && !input.Contains(')'))
        {
            return true;
        }

        int lastOpen = input.LastIndexOf('(');
        if (lastOpen == -1) return false;
        int firstClosedAfter = input.Substring(lastOpen).IndexOf(')');
        if (firstClosedAfter == -1) return false;
        input = input.Remove(firstClosedAfter+lastOpen, 1);
        input = input.Remove(lastOpen, 1);
        return IsBalanced(input);
    }

    static List<Token> GetTokens(string input){
        string token = "";
        tokens = new List<Token>();
        bool isPlainText = false;
        if (input.Trim().Last() != ';')
        {
            throw new SyntaxException("Missing Semicolon at he end of the sentence");
        }
        foreach(char c in input+" "){ 
            if(char.IsWhiteSpace(c) && !isPlainText){
                if(token != ""){
                    tokens.Add(new Token(token, ClasificarToken(token)));
                    token = "";
                }
            }else if(symbols.Contains(c.ToString())){
                if (c == '"')
                {
                    if (isPlainText)
                    {
                        tokens.Add(new Token(token, Token.TokenType.Text));
                        token = "";
                        isPlainText = false;
                    }
                    else
                    {
                        if(token != ""){
                            tokens.Add(new Token(token, ClasificarToken(token)));
                            token = "";
                        }
                        isPlainText = true;
                    }
                    continue;
                }

                if (isPlainText)
                {
                    token += c;
                    continue;
                }
                if(token != "")
                    tokens.Add(new Token(token, ClasificarToken(token)));

                if (c == '=')
                {
                    if(tokens.Last().type == Token.TokenType.Igual)
                    {
                        tokens.Remove(tokens.Last());
                        tokens.Add(new Token("==", Token.TokenType.Comparar));
                        continue;
                    }
                    if (tokens.Last().type == Token.TokenType.Negation)
                    {
                        tokens.Remove(tokens.Last());
                        tokens.Add(new Token("!=", Token.TokenType.Different));
                        continue;
                    }
                    
                }
                if (c == '>' && tokens.Last().type == Token.TokenType.Igual)
                {
                    tokens.Remove(tokens.Last());
                    tokens.Add(new Token("=>", Token.TokenType.Arrow));
                    continue;
                }
                tokens.Add(new Token(c.ToString(), ClasificarToken(c.ToString())));

                token = "";

            }else{
                token += c;
            }
        }

        if (isPlainText)
        {
            throw new SyntaxException("Missing closing Quotation Mark (\") after '"+token+"'.");
        }
        return tokens;
    }
    static Token.TokenType ClasificarToken(string token){
        try{
            double.Parse(token);
            return Token.TokenType.Number;
        }catch(FormatException){
            
            switch(token.ToLower()){
                case "+":
                    return Token.TokenType.Sum;
                case "-":
                    return Token.TokenType.Resta;
                case "*":
                    return Token.TokenType.Product;
                case "/":
                    return Token.TokenType.Div;
                case "^":
                    return Token.TokenType.Pow;
                case "sqrt":
                    return Token.TokenType.Sqrt;
                case "(":
                    return Token.TokenType.OpenParenthesis;
                case ")":
                    return Token.TokenType.CloseParenthesis;
                case "=":
                    return Token.TokenType.Igual;
                case ">":
                    return Token.TokenType.MayorQ;
                case "<":
                    return Token.TokenType.MenorQ;
                case "!":
                    return Token.TokenType.Negation;
                case "&":
                    return Token.TokenType.And;
                case "|":
                    return Token.TokenType.Or;
                case "%":
                    return Token.TokenType.Resto;
                case ";":
                    return Token.TokenType.Semicolon;
                case "true":
                case "false":
                    return Token.TokenType.Bool;
                case "sin":
                    return Token.TokenType.Sin;
                case "cos":
                    return Token.TokenType.Cos;
                case "tan":
                    return Token.TokenType.Tan;
                case "cot":
                    return Token.TokenType.Cot;
                case "log":
                    return Token.TokenType.Log;
                case "print":
                    return Token.TokenType.Print;
                case "let":
                    return Token.TokenType.VarDeclarationKeyWord;
                case "in":
                    return Token.TokenType.VarInKeyWord;
                case "function":
                    return Token.TokenType.FunDeclarationKeyWord;
                case "@":
                    return Token.TokenType.Concat;
                case ",":
                    return Token.TokenType.Comma;
                case "if":
                    return Token.TokenType.If;
                case "else":
                    return Token.TokenType.Else;
                default:
                    if(char.IsDigit(token.First()))
                        throw new LexicalException(token);
                    
                    return Token.TokenType.ID;
            }
        }
    }

    
    
}
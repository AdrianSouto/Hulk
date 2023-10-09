static class Lexer{
    public static List<Token> tokens;
    static string[] symbols = {"+", "-", "/", "*", "(", ")", "=","\""};
    public static void ApplyLexer(string input){
        tokens = GetTokens(input);
    }
    static List<Token> GetTokens(string input){
        string token = "";
        List<Token> tokens = new List<Token>();
        bool isPlainText = false;
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
                        isPlainText = true;
                    continue;
                }

                if (isPlainText) continue;
                if(token != "")
                    tokens.Add(new Token(token, ClasificarToken(token)));
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
            if(KeyWord.Keywords.Contains(token))
                return Token.TokenType.KeyWord;
            if (MyFunction<string>.PredFunctions.Contains(token))
            {
                return Token.TokenType.PredFunction;
            }

            switch(token){
                case "+":
                    return Token.TokenType.Sum;
                case "-":
                    return Token.TokenType.Resta;
                case "*":
                    return Token.TokenType.Mult;
                case "/":
                    return Token.TokenType.Div;
                case "^":
                    return Token.TokenType.Potencia;
                case "(":
                    return Token.TokenType.OpenPharentesis;
                case ")":
                    return Token.TokenType.ClosePharentesis;
                case "=":
                    return Token.TokenType.Igual;
                default:
                    if(char.IsDigit(token.First()))
                        throw new LexicalException(token);
                    
                    return Token.TokenType.ID;
            }
        }
    }

    
    
}
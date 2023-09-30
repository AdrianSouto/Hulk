static class Lexer{
    public static List<Token> tokens = new List<Token>();
    static string[] symbols = {"+", "-", "/", "*", "(", ")", "=","\""};
    public static void ApplyLexer(string input){
        string token = "";

        foreach(char c in input+" "){
            if(char.IsWhiteSpace(c)){
                if(token != ""){
                    tokens.Add(new Token(token, ClasificarToken(token)));
                    token = "";
                }
            }else if(symbols.Contains(c.ToString())){
                if(token != "")
                    tokens.Add(new Token(token, ClasificarToken(token)));
                tokens.Add(new Token(c.ToString(), ClasificarToken(c.ToString())));
                token = "";
            }else{
                token += c;
            }
        }
    }
    static Token.TokenType ClasificarToken(string token){
        try{
            double.Parse(token);
            return Token.TokenType.Number;
        }catch(FormatException){
            if(KeyWord.keywords.Contains(token))
                return Token.TokenType.KeyWord;

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
                case ")":
                    return Token.TokenType.Pharentesis;
                case "=":
                    return Token.TokenType.Igual;
                default:
                    return Token.TokenType.Word;
            }
        }
    }
    
}
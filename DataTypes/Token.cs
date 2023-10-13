public class Token{

    public enum TokenType
    {
        Number,
        Sum,
        Resta,
        Product,
        Div,
        Pow,
        OpenParenthesis,
        CloseParenthesis,
        KeyWord,
        ID,
        Igual,
        Text,
        PredFunction,
        Semicolon,
        Sin,
        Cos,
        Tan,
        Cot,
        Sqrt,
        Log,
        Print,
        VarDeclarationKeyWord,
        VarInKeyWord,
        FunDeclarationKeyWord,
        Concat,
        Comma,
        If,
        Else,
        Comparar,
        Negation,
        Different,
        And,
        Or,
        Resto,
        Bool,
        MayorQ,
        Arrow,
        MenorQ
    }

    public string value{get;}
    public TokenType type{get;}

    public Token(string value, TokenType type){
        this.value = value;
        this.type = type;
    } 
}
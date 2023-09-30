class Token{

    public enum TokenType{Number, Sum, Resta, Mult, Div, Potencia, Pharentesis, KeyWord, Word, Igual}
    public string value{get;}
    public TokenType type{get;}

    public Token(string value, TokenType type){
        this.value = value;
        this.type = type;
    } 
}
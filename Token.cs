class Token{

    public enum TokenType{Number, Sum, Resta, Mult, Div, Potencia, OpenPharentesis, ClosePharentesis, KeyWord, ID, Igual, Text, PredFunction}
    public string value{get;}
    public TokenType type{get;}

    public Token(string value, TokenType type){
        this.value = value;
        this.type = type;
    } 
}
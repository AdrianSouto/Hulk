namespace Hulk;
class Program
{
    static void Main(string[] args)
    {
        string input =  Console.ReadLine();
        Lexer.ApplyLexer(input);
        foreach(Token token in Lexer.tokens){
            System.Console.WriteLine(token.value+" - "+token.type.ToString());
        }
    }

}

using Hulk.Model;

namespace Hulk;
class Program{
    static void Main(string[] args){
        while(true){
            Console.Write(">");
            string input =  Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                try{
                    Lexer.ApplyLexer(input);
                    Parser p = new Parser(Lexer.tokens);
                    Console.WriteLine(p.Evaluate());
                }catch(MyException e){
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Para mas info visite: "+e.HelpLink);
                }
            }
            
        }
    }

}

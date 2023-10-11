using Hulk.Model;

namespace Hulk;
class Program{
    static void Main(string[] args){
        while(true){
            Console.Write(">");
            string input =  Console.ReadLine();
            try{
            Lexer.ApplyLexer(input);
            MathParser p = new MathParser(Lexer.tokens);
            Console.WriteLine(p.value);
            }catch(MyException e){
                Console.WriteLine(e.Message);
                Console.WriteLine("Para mas info visite: "+e.HelpLink);
            }
        }
    }

}

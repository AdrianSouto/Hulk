using Hulk.Model;

namespace Hulk;
class Program{
    static void Main(string[] args){
        while(true){
            Console.Write(">");
            string input =  Console.ReadLine();
            try{
            Lexer.ApplyLexer(input);
            Parser p = new Parser(Lexer.tokens);
            Console.WriteLine(p.value);
            
            /*foreach (Token t in Lexer.tokens)
            {
                Console.WriteLine(t.value+" - "+t.type.ToString());
            }*/
            }catch(MyException e){
                Console.WriteLine(e.Message);
                Console.WriteLine("Para mas info visite: "+e.HelpLink);
            }
        }
    }

}

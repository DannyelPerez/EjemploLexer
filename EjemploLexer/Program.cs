using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EjemploLexer
{
    class Program
    {
        static void Main(string[] args)
        {
            var lex = new Lexer(@"cont = cont + 1;
            cont2 = 0;");
            var currentToken = lex.GetNextToken();
            while (currentToken.Type != TokenTypes.EOF)
            {
                Console.WriteLine(currentToken.ToString());
                currentToken = lex.GetNextToken();
            }
            Console.ReadKey();

        }
    }
}

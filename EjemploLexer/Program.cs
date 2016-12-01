using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EjemploLexer.Sintatico;

namespace EjemploLexer
{
    class Program
    {
        static void Main(string[] args)
        {
            var hoySi = System.IO.File.ReadAllText(@"C:\Users\danny\Documents\Test\hola.html");
            var lex = new Lexer(hoySi);
            Parser parser = new Parser(lex);
            var root = parser.Parse();
            foreach (var statementNode in root)
            {
                statementNode.ValidSemantic();
            }
            Console.ReadKey();

        }
    }
}

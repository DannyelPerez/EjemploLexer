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
            var lex = new Lexer(@"read a;
            read b;
            print a+b;");
            Parser parser = new Parser(lex);
            var root = parser.Parse();
            foreach (var statementNode in root)
            {
                statementNode.Interpret();
            }
            Console.ReadKey();

        }
    }
}

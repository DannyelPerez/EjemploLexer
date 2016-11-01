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
            var lex = new Lexer(@"cont = cont + 1;
            cont2 = 0;
            print;");
            Parser parser = new Parser(lex);
            parser.Parse();
            Console.ReadKey();

        }
    }
}

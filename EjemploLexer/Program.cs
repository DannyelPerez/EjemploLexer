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
            var lex = new Lexer(@"cont = 1;
            cont2 = cont + 1;
            print cont2*2;");
            Parser parser = new Parser(lex);
            parser.Parse();
            Console.ReadKey();

        }
    }
}

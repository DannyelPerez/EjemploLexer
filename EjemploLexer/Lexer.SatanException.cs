using System;

namespace EjemploLexer
{
    public partial class Lexer
    {
        public class SatanException : Exception
        {
            public SatanException(string nasty):base(nasty)
            {
                
            }
        }
    }
}
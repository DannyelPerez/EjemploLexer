using System;
using System.Collections.Generic;

namespace EjemploLexer
{
     public partial class Lexer
     {
         private string _sourceCode;
         private int _currentPointer;
         private Dictionary<string, TokenTypes> _reserveWords;
        public Lexer(string sourceCode)
        {
            _sourceCode = sourceCode;
            _currentPointer = 0;
            _reserveWords = new Dictionary<string, TokenTypes>();
            _reserveWords.Add("print", TokenTypes.PR_PRINT);

        }

        public Token GetNextToken()
        {
            var tmp = GetCurrentSymbol();
            var lexeme = "";

            while (Char.IsWhiteSpace(tmp))
            {
                _currentPointer++;
                tmp = GetCurrentSymbol();
            }

            if (tmp == '\0')
            {
                return new Token {Type = TokenTypes.EOF};
            }
            
            if (Char.IsLetter(tmp))
            {
                lexeme += tmp;
                _currentPointer++;
                return GetId(lexeme);
           
            }

            if (Char.IsDigit(tmp))
            {
                lexeme += tmp;
                _currentPointer++;
                return GetDigit(lexeme);
            }

            throw new SatanException("Nasty");
        }

         private Token GetDigit(string lexeme)
         {
            var tmp1 = GetCurrentSymbol();
            while (Char.IsDigit(tmp1))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }
            return new Token { Type = TokenTypes.Digit, Lexeme = lexeme };
        }

         private Token GetId(string lexeme)
         {
             var tmp1 = GetCurrentSymbol();
             while (Char.IsLetterOrDigit(tmp1))
             {
                 lexeme += tmp1;
                 _currentPointer++;
                 tmp1 = GetCurrentSymbol();
             }

             return new Token {Type = _reserveWords.ContainsKey(lexeme)?_reserveWords[lexeme]:TokenTypes.ID
                 , Lexeme = lexeme};
         }

         private char GetCurrentSymbol()
         {
             if (_currentPointer < _sourceCode.Length)
             {
                 return _sourceCode[_currentPointer];
             }
             return '\0';
         }
    }
}
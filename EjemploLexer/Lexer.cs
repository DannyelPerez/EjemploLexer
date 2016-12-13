using System;
using System.Collections.Generic;

namespace EjemploLexer
{
     public partial class Lexer
     {
         private string _sourceCode;
         private int _currentPointer;
         private Dictionary<string, TokenTypes> _reserveWords;
         private Dictionary<string, TokenTypes> _symbols;
        public Lexer(string sourceCode)
        {
            _sourceCode = sourceCode;
            _currentPointer = 0;
            _reserveWords = new Dictionary<string, TokenTypes>();
            _reserveWords.Add("print", TokenTypes.PR_PRINT);
            _reserveWords.Add("read", TokenTypes.PR_READ);
            _reserveWords.Add("false", TokenTypes.LIT_BOOL);
            _reserveWords.Add("true", TokenTypes.LIT_BOOL);
            _reserveWords.Add("for", TokenTypes.PR_FOR);
            _reserveWords.Add("while", TokenTypes.PR_WHILE);
            _reserveWords.Add("if", TokenTypes.PR_IF);
            _reserveWords.Add("to", TokenTypes.PR_TO);
            _reserveWords.Add("end", TokenTypes.PR_END);
            _reserveWords.Add("begin", TokenTypes.PR_BEGIN);
            _reserveWords.Add("else", TokenTypes.PR_ELSE);
            _symbols = new Dictionary<string, TokenTypes>();
            _symbols.Add("+", TokenTypes.OP_SUM);
            _symbols.Add("-", TokenTypes.OP_SUB);
            _symbols.Add("*", TokenTypes.OP_MUL);
            _symbols.Add("/", TokenTypes.OP_DIV);
            _symbols.Add("=", TokenTypes.OP_EQU);
            _symbols.Add(";", TokenTypes.FN_STM);
            _symbols.Add("(", TokenTypes.PAR_LEFT);
            _symbols.Add(")", TokenTypes.PAR_RIGHT);
            _symbols.Add("{", TokenTypes.CB_LEFT);
            _symbols.Add("}", TokenTypes.CB_RIGHT);


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

            if (_symbols.ContainsKey(tmp.ToString()))
            {
                _currentPointer++;
                return new Token {Type = _symbols[tmp.ToString()],Lexeme = tmp.ToString()};
            }

            if (tmp == '\"')
            {
                _currentPointer++;
                return LiteralString();
            }

            throw new SatanException("Nasty");
        }

         private Token LiteralString()
         {
             var lexeme = "";
             var currentSymbol = GetCurrentSymbol();
             while (currentSymbol != '\"')
             {
                 lexeme += currentSymbol;
                 _currentPointer++;
                 currentSymbol = GetCurrentSymbol();
             }
             _currentPointer++;
             return new Token {Lexeme = lexeme,Type = TokenTypes.LIT_STRING};
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
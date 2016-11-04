using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploLexer.Sintatico
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currenToken;
        private Dictionary<string, int> _variables;
        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currenToken = lexer.GetNextToken();
            _variables = new Dictionary<string, int>();
        }


        public void Parse()
        {
            Codigo();
            if(_currenToken.Type!=TokenTypes.EOF)
                throw new Exception("Se esperaba Eof");

        }

        private void Codigo()
        {
            ListaSentencias();
;
        }

        private void ListaSentencias()
        {
            //Lista_Sentencias->Sentencia Lista_Sentencias
            if (_currenToken.Type == TokenTypes.ID || _currenToken.Type == TokenTypes.PR_PRINT)
            {
                Sentencia();
                ListaSentencias();
            }
            //Lista_Sentencia->Epsilon
            else
            {
                
            }
            
        }

        private void Sentencia()
        {
            //id = expresion ;
            if (_currenToken.Type == TokenTypes.ID)
            {
                var lexemeid = _currenToken.Lexeme;
                _currenToken = _lexer.GetNextToken();
                if(_currenToken.Type!=TokenTypes.OP_EQU)
                    throw new Exception("Se esperaba = ");
                _currenToken = _lexer.GetNextToken();
                var expresionValor = Expresion();
                if(_currenToken.Type!=TokenTypes.FN_STM)
                    throw  new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();
                _variables[lexemeid] = expresionValor;

            }//print expresion ;
            else if (_currenToken.Type == TokenTypes.PR_PRINT)
            {
                _currenToken = _lexer.GetNextToken();
                var expresionValor =Expresion();
                if (_currenToken.Type != TokenTypes.FN_STM)
                    throw new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();
                Console.Write(expresionValor);
            }
            else
            {
                throw new Exception("Se esperaba un id o print");
            }
        }

        private int Expresion()
        {
            var termValue = Term();
            return ExpresionP(termValue);
        }

        private int ExpresionP(int param)
        {
            //+term ExpresionP
            if (_currenToken.Type == TokenTypes.OP_SUM)
            {
                _currenToken = _lexer.GetNextToken();
                var termValue =Term();
                return ExpresionP(param +termValue);
            }
            //-term ExpresionP
            else if (_currenToken.Type == TokenTypes.OP_SUB)
            {
                _currenToken = _lexer.GetNextToken();
                var termValue = Term();
                return ExpresionP(param - termValue);
            }
            // Epsilon
            else
            {
                return param;
            }
        }

        private int Term()
        {
            var factorValue = Factor();
            return TermP(factorValue);
        }
        private int TermP(int param)
        {
            //*Factor TermP
            if (_currenToken.Type == TokenTypes.OP_MUL)
            {
                _currenToken = _lexer.GetNextToken();
                var factorValue = Factor();
                return TermP(param * factorValue);
            }
            // / Factor TermP
            else if (_currenToken.Type == TokenTypes.OP_DIV)
            {
                _currenToken = _lexer.GetNextToken();
                var factorValue = Factor();
                return TermP(param / factorValue);
            }
            // Epsilon
            else
            {
                return param;
            }
        }

        private int Factor()
        {
            if (_currenToken.Type == TokenTypes.ID)
            {
                var lexeme = _currenToken.Lexeme;
                _currenToken = _lexer.GetNextToken();
                return _variables[lexeme];

            }
            else if (_currenToken.Type == TokenTypes.Digit)
            {
                var numValor = int.Parse(_currenToken.Lexeme);
                _currenToken = _lexer.GetNextToken();
                return numValor;

            }
            else if (_currenToken.Type == TokenTypes.Left_Par)
            {
                _currenToken = _lexer.GetNextToken();

                var expressionValor = Expresion();
                if (_currenToken.Type != TokenTypes.Right_Par)
                    throw new Exception("Se esperaba )");

                _currenToken = _lexer.GetNextToken();
                return expressionValor;
            }
            else
            {
                throw new Exception("Se esperaba un Factor");
            }

        }

    }
}

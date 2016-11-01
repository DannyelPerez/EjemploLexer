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
        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currenToken = lexer.GetNextToken();
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
                _currenToken = _lexer.GetNextToken();
                if(_currenToken.Type!=TokenTypes.OP_EQU)
                    throw new Exception("Se esperaba = ");
                _currenToken = _lexer.GetNextToken();
                Expresion();
                if(_currenToken.Type!=TokenTypes.FN_STM)
                    throw  new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();

            }//print expresion ;
            else if (_currenToken.Type == TokenTypes.PR_PRINT)
            {
                _currenToken = _lexer.GetNextToken();
                Expresion();
                if (_currenToken.Type != TokenTypes.FN_STM)
                    throw new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();
            }
            else
            {
                throw new Exception("Se esperaba un id o print");
            }
        }

        private void Expresion()
        {
            Term();
            ExpresionP();
        }

        private void ExpresionP()
        {
            //+term ExpresionP
            if (_currenToken.Type == TokenTypes.OP_SUM)
            {
                _currenToken = _lexer.GetNextToken();
                Term();
                ExpresionP();
            }
            //-term ExpresionP
            else if (_currenToken.Type == TokenTypes.OP_SUB)
            {
                _currenToken = _lexer.GetNextToken();
                Term();
                ExpresionP();
            }
            // Epsilon
            else
            {
                
            }
        }

        private void Term()
        {
            Factor();
            TermP();
        }
        private void TermP()
        {
            //*Factor TermP
            if (_currenToken.Type == TokenTypes.OP_MUL)
            {
                _currenToken = _lexer.GetNextToken();
                Factor();
                TermP();
            }
            // / Factor TermP
            else if (_currenToken.Type == TokenTypes.OP_DIV)
            {
                _currenToken = _lexer.GetNextToken();
                Factor();
                TermP();
            }
            // Epsilon
            else
            {

            }
        }

        private void Factor()
        {
            if (_currenToken.Type == TokenTypes.ID)
            {
                _currenToken = _lexer.GetNextToken();

            }
            else if (_currenToken.Type == TokenTypes.Digit)
            {
                _currenToken = _lexer.GetNextToken();

            }
            else if (_currenToken.Type == TokenTypes.Left_Par)
            {
                _currenToken = _lexer.GetNextToken();
                Expresion();
                if (_currenToken.Type == TokenTypes.Right_Par)
                    _currenToken = _lexer.GetNextToken();
                else
                {
                    throw new Exception("Se esperaba )");
                }
            }
            else
            {
                throw new Exception("Se esperaba un Factor");
            }
        }

    }
}

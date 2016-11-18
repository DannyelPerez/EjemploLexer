using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Arbol.Sentencia;

namespace EjemploLexer.Sintatico
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currenToken;
        public static Dictionary<string, int> _variables;
        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _currenToken = lexer.GetNextToken();
            _variables = new Dictionary<string, int>();
        }


        public List<StatementNode> Parse()
        {
            var code = Codigo();
            if(_currenToken.Type!=TokenTypes.EOF)
                throw new Exception("Se esperaba Eof");
            return code;
        }

        private List<StatementNode> Codigo()
        {
            return ListaSentencias();
 ;
        }

        private List<StatementNode> ListaSentencias()
        {
            //Lista_Sentencias->Sentencia Lista_Sentencias
            if (_currenToken.Type == TokenTypes.ID || _currenToken.Type == TokenTypes.PR_PRINT ||_currenToken.Type == TokenTypes.PR_READ)
            {
                var statement = Sentencia();
                var statementList = ListaSentencias();
                statementList.Insert(0,statement);
                return statementList;
            }
            //Lista_Sentencia->Epsilon
            else
            {
                return new List<StatementNode>();
            }
            
        }

        private StatementNode Sentencia()
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
                return new AssignationNode
                {
                    LeftValue = new IdNode {Name = lexemeid},
                    RightValue = expresionValor
                };

            }//print expresion ;
            else if (_currenToken.Type == TokenTypes.PR_PRINT)
            {
                _currenToken = _lexer.GetNextToken();
                var expresionValor =Expresion();
                if (_currenToken.Type != TokenTypes.FN_STM)
                    throw new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();
                
                return new PrintNode {Value = expresionValor};
            }//read expresion;
            else if (_currenToken.Type == TokenTypes.PR_READ)
            {
                _currenToken = _lexer.GetNextToken();
                if (_currenToken.Type != TokenTypes.ID)
                    throw new Exception("Se esperaba un Id");
                var lexemeId = _currenToken.Lexeme;
                
                _currenToken = _lexer.GetNextToken();
                if (_currenToken.Type != TokenTypes.FN_STM)
                    throw new Exception("Se esperaba un ;");
                _currenToken = _lexer.GetNextToken();

                return new ReadNode {Variable = new IdNode {Name = lexemeId} };
            }

            else
            {
                throw new Exception("Se esperaba un id o print");
            }
        }

        private ExpressionNode Expresion()
        {
            var termValue = Term();
            return ExpresionP(termValue);
        }

        private ExpressionNode ExpresionP(ExpressionNode param)
        {
            //+term ExpresionP
            if (_currenToken.Type == TokenTypes.OP_SUM)
            {
                _currenToken = _lexer.GetNextToken();
                var termValue =Term();
                return ExpresionP(new SumNode {LeftOperand = param, RightOperand = termValue});
            }
            //-term ExpresionP
            else if (_currenToken.Type == TokenTypes.OP_SUB)
            {
                _currenToken = _lexer.GetNextToken();
                var termValue = Term();
                return ExpresionP(new SubNode {LeftOperand = param, RightOperand = termValue});
            }
            // Epsilon
            else
            {
                return param;
            }
        }

        private ExpressionNode Term()
        {
            var factorValue = Factor();
            return TermP(factorValue);
        }
        private ExpressionNode TermP(ExpressionNode param)
        {
            //*Factor TermP
            if (_currenToken.Type == TokenTypes.OP_MUL)
            {
                _currenToken = _lexer.GetNextToken();
                var factorValue = Factor();
                return TermP(new MulNode {LeftOperand = param, RightOperand = factorValue});
            }
            // / Factor TermP
            else if (_currenToken.Type == TokenTypes.OP_DIV)
            {
                _currenToken = _lexer.GetNextToken();
                var factorValue = Factor();
                return TermP(new DivNode {LeftOperand = param, RightOperand = factorValue});
            }
            // Epsilon
            else
            {
                return param;
            }
        }

        private ExpressionNode Factor()
        {
            if (_currenToken.Type == TokenTypes.ID)
            {
                var lexeme = _currenToken.Lexeme;
                _currenToken = _lexer.GetNextToken();
                return new IdNode {Name = lexeme};

            }
            else if (_currenToken.Type == TokenTypes.Digit)
            {
                var numValor = int.Parse(_currenToken.Lexeme);
                _currenToken = _lexer.GetNextToken();
                return new NumberLiteralNode {Value = numValor};

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

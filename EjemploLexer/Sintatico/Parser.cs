﻿using System;
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
            if (_currenToken.Type == TokenTypes.ID 
                || _currenToken.Type == TokenTypes.PR_PRINT 
                ||_currenToken.Type == TokenTypes.PR_READ
                ||_currenToken.Type == TokenTypes.PR_FOR
                ||_currenToken.Type == TokenTypes.PR_WHILE
                ||_currenToken.Type == TokenTypes.PR_IF)
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
            }//For
            else if (_currenToken.Type == TokenTypes.PR_FOR)
            {
                _currenToken = _lexer.GetNextToken();
                if(_currenToken.Type != TokenTypes.ID)
                    throw new Exception("Se esperaba un id");
                var id = _currenToken;
                _currenToken = _lexer.GetNextToken();
                if(_currenToken.Type != TokenTypes.OP_EQU)
                    throw new Exception("Se esperaba un =");
                _currenToken = _lexer.GetNextToken();
                var exp1 = Expresion();
                if(_currenToken.Type != TokenTypes.PR_TO)
                    throw new Exception("Se esperaba pr to");
                _currenToken = _lexer.GetNextToken();
                var exp2 = Expresion();
                if(_currenToken.Type != TokenTypes.PR_BEGIN)
                    throw new Exception("Se esperaba pr begin");
                _currenToken = _lexer.GetNextToken();
                var ls = ListaSentencias();
                if(_currenToken.Type != TokenTypes.PR_END)
                    throw new Exception("Se esperaba end");
                _currenToken = _lexer.GetNextToken();
                return new ForNode {Id = new IdNode {Name = id.Lexeme},
                    FromExp = exp1,
                    ToExp = exp2,
                    StatementList = ls
                };
            }
            else if (_currenToken.Type == TokenTypes.PR_WHILE)
            {
                _currenToken = _lexer.GetNextToken();
                var condition = Expresion();
                if (_currenToken.Type != TokenTypes.PR_BEGIN)
                    throw new Exception("Se esperaba pr begin");
                _currenToken = _lexer.GetNextToken();
                var ls = ListaSentencias();
                if (_currenToken.Type != TokenTypes.PR_END)
                    throw new Exception("Se esperaba end");
                _currenToken = _lexer.GetNextToken();
                return new WhileNode {Conditional = condition, StatementList = ls};
            }
            else if (_currenToken.Type == TokenTypes.PR_IF)
            {
                _currenToken = _lexer.GetNextToken();
                var condition = Expresion();
                if (_currenToken.Type != TokenTypes.PR_BEGIN)
                    throw new Exception("Se esperaba pr begin");
                _currenToken = _lexer.GetNextToken();
                var lsTrue = ListaSentencias();
                if (_currenToken.Type != TokenTypes.PR_END)
                    throw new Exception("Se esperaba end");
                _currenToken = _lexer.GetNextToken();
                if(_currenToken.Type != TokenTypes.PR_ELSE)
                    return new IfNode { Conditional = condition, StatementListTrue = lsTrue };
                _currenToken = _lexer.GetNextToken();
                if (_currenToken.Type != TokenTypes.PR_BEGIN)
                    throw new Exception("Se esperaba pr begin");
                _currenToken = _lexer.GetNextToken();
                var lsFalse = ListaSentencias();
                if (_currenToken.Type != TokenTypes.PR_END)
                    throw new Exception("Se esperaba end");
                _currenToken = _lexer.GetNextToken();
                return new IfNode { Conditional = condition,
                    StatementListTrue = lsTrue,
                    StatementListFalse = lsFalse };
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
            else if (_currenToken.Type == TokenTypes.LIT_STRING)
            {
                var stringValue = _currenToken.Lexeme;
                _currenToken = _lexer.GetNextToken();
                return new StringLiteral {Value = stringValue};
            }
            else if (_currenToken.Type == TokenTypes.LIT_BOOL)
            {
                var boolValue = _currenToken.Lexeme == "true";
                _currenToken = _lexer.GetNextToken();
                return new BoolLiteral {Value = boolValue};
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

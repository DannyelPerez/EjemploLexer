using System.Collections.Generic;
using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class IfNode : StatementNode
    {
        public ExpressionNode Conditional { get; set; }
        public List<StatementNode> StatementListTrue { get; set; }
        public List<StatementNode> StatementListFalse { get; set; }
        public override void ValidSemantic()
        {
            var conditional = Conditional.EvaluateSemantic();

            if (!(conditional is BoolTipo))
                throw new SemanticException($"Condicion debe de ser tipo Bool y no {conditional}");
            foreach (var statementNode in StatementListTrue)
            {
                statementNode.ValidSemantic();
            }

            if(StatementListFalse != null)
                foreach (var statementNode in StatementListFalse)
                {
                    statementNode.ValidSemantic();
                }
        }

        public override void Interpret()
        {
            var condition = (BoolValue)Conditional.Interpret();
            if (condition.Value)
            {
                foreach (var statementNode in StatementListTrue)
                {
                    statementNode.Interpret();
                }
            }
            else
            {
                if (StatementListFalse != null)
                {
                    foreach (var statementNode in StatementListFalse)
                    {
                        statementNode.Interpret();
                    }
                }
            }

        }
    }
}
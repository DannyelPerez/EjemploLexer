using System.Collections.Generic;
using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class WhileNode : StatementNode
    {
        public ExpressionNode Conditional { get; set; }
        public List<StatementNode> StatementList { get; set; }
        public override void ValidSemantic()
        {
            var conditional  = Conditional.EvaluateSemantic();
            
            if (!(conditional is BoolTipo))
                throw new SemanticException($"Condicion debe de ser tipo Bool y no {conditional}");
            foreach (var statementNode in StatementList)
            {
                statementNode.ValidSemantic();
            }
        }

        public override void Interpret()
        {
            var condition = Conditional.Interpret();
            while (((BoolValue)condition).Value )
            {
                foreach (var statementNode in StatementList)
                {
                    statementNode.Interpret();
                }
            }
        }
    }
}
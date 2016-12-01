using System;
using EjemploLexer.Semantico.Arbol.Expresion;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class PrintNode:StatementNode
    {
        public ExpressionNode Value { get; set; }

        public override void ValidSemantic()
        {
            Value.EvaluateSemantic();
        }
    }
}
using System;
using EjemploLexer.Semantico.Arbol.Expresion;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class PrintNode:StatementNode
    {
        public ExpressionNode Value { get; set; }
        public override void Interpret()
        {
            Console.WriteLine(Value.Evaluate());
        }
    }
}
using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class NumberLiteralNode: ExpressionNode
    {
        public int Value { get; set; }

        public override Tipo EvaluateSemantic()
        {
            return new IntTipo();
        }

        public override Value Interpret()
        {
            return new IntValue { Value = Value };
        }
    }
}
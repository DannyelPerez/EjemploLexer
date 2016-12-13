using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class StringLiteral: ExpressionNode
    {
        public string Value { get; set; }
        public override Tipo EvaluateSemantic()
        {
            return new StringTipo();
        }

        public override Value Interpret()
        {
            return new StringValue { Value = Value };
        }
    }
}
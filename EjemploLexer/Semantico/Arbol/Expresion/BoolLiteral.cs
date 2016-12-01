using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class BoolLiteral: ExpressionNode
    {
        public bool Value { get; set; }
        public override Tipo EvaluateSemantic()
        {
            return new BoolTipo();
        }
    }
}
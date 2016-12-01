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
    }
}
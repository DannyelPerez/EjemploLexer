namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class NumberLiteralNode: ExpressionNode
    {
        public int Value { get; set; }
        public override int Evaluate()
        {
            return Value;
        }
    }
}
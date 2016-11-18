namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class MulNode: BinaryOperatorNode
    {
        public override int Evaluate()
        {
            return LeftOperand.Evaluate() * RightOperand.Evaluate();
        }
    }
}
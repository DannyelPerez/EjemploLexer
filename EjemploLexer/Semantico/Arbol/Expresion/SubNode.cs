namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class SubNode: BinaryOperatorNode
    {
        public override int Evaluate()
        {
            return LeftOperand.Evaluate() - RightOperand.Evaluate();
        }
    }
}
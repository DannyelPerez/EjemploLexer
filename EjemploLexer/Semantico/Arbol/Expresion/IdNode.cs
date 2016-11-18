using EjemploLexer.Sintatico;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class IdNode: ExpressionNode
    {
        public string Name { get; set; }
        public override int Evaluate()
        {
            return Parser._variables[Name];
        }
    }
}
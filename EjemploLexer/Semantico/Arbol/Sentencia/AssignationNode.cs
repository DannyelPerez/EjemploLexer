using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Sintatico;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class AssignationNode: StatementNode
    {
        public IdNode LeftValue { get; set; }
        public ExpressionNode RightValue { get; set; }
        public override void Interpret()
        {
            Parser._variables[LeftValue.Name] = RightValue.Evaluate();
        }
    }
}
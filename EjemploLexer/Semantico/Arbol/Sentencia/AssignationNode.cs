using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Tipos;
using EjemploLexer.Sintatico;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class AssignationNode: StatementNode
    {
        public IdNode LeftValue { get; set; }
        public ExpressionNode RightValue { get; set; }

        public override void ValidSemantic()
        {
            var rTipo = RightValue.EvaluateSemantic();
            if(!TablaSimbolos.Instance.VariableExist(LeftValue.Name))
                TablaSimbolos.Instance.DeclareVariable(LeftValue.Name,rTipo);
            else
            {
                var lTipo = TablaSimbolos.Instance.GetVariable(LeftValue.Name);
                if(lTipo.GetType() != rTipo.GetType())
                    throw new SemanticException($"No se puede asignar {rTipo} a {lTipo}");
            }
        }
    }
}
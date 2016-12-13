using System.Collections.Generic;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class ForNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExpressionNode FromExp { get; set; }
        public ExpressionNode ToExp { get; set; }
        public List<StatementNode> StatementList { get; set; }

        public override void ValidSemantic()
        {
            var fromExpT = FromExp.EvaluateSemantic();
            var toExpT = ToExp.EvaluateSemantic();
            if(!(fromExpT is IntTipo) || !(toExpT is IntTipo))
                throw new SemanticException($"Expresion From y To deben ser tipo int, FromT: {fromExpT} ToT:{toExpT}");
            var idT = Id.EvaluateSemantic();
            if(!(idT is IntTipo))
                throw new SemanticException($"Id debe de ser de tipo Int, idT: {idT}");
            foreach (var statementNode in StatementList)
            {
                statementNode.ValidSemantic();
            }
        }

        public override void Interpret()
        {
            throw new System.NotImplementedException();
        }
    }
}
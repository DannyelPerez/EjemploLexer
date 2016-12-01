using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class DivNode: BinaryOperatorNode
    {
        public override Tipo EvaluateSemantic()
        {
            var ltipo = LeftOperand.EvaluateSemantic();
            var rtipo = RightOperand.EvaluateSemantic();
            if (ltipo.GetType() == rtipo.GetType())
            {
                if (ltipo is IntTipo)
                    return ltipo;

            }
            throw new SemanticException($"No se puede dividir entre tipo: {ltipo} y {rtipo}");
        }
    }
}

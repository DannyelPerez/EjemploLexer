using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class SumNode: BinaryOperatorNode
    {
        public override Tipo EvaluateSemantic()
        {
            var ltipo = LeftOperand.EvaluateSemantic();
            var rtipo = RightOperand.EvaluateSemantic();
            if (ltipo.GetType() == rtipo.GetType())
            {
                if (ltipo is IntTipo || ltipo is StringTipo || ltipo is BoolTipo)
                    return ltipo;
                
            }
            throw new SemanticException($"No se puede sumar entre tipo: {ltipo} y {rtipo}");
        }

        public override Value Interpret()
        {
            var leftV = LeftOperand.Interpret();
            var rightV = RightOperand.Interpret();
            if (leftV is IntValue)
            {
                return new IntValue
                {
                    Value = ((IntValue)leftV).Value + ((IntValue)rightV).Value
                };
            }

            if (leftV is StringValue)
            {
                return new StringValue
                {
                    Value = ((StringValue)leftV).Value + ((StringValue)rightV).Value
                };
            }

            throw new NotImplementedException();

        }
    }
}

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
            if (ltipo.GetType() != rtipo.GetType())
                throw new SemanticException($"No se puede sumar entre tipo: {ltipo} y {rtipo}");
            if (ltipo is IntTipo || ltipo is StringTipo || ltipo is BoolTipo)
                return ltipo;
            throw new SemanticException($"No se puede sumar entre tipo: {ltipo} y {rtipo}");
        }

        public override Value Interpret()
        {
            var leftV = LeftOperand.Interpret();
            var rightV = RightOperand.Interpret();
            var value = leftV as IntValue;
            if (value != null)
            {
                return new IntValue
                {
                    Value = value.Value + ((IntValue)rightV).Value
                };
            }

            var v = leftV as StringValue;
            if (v != null)
            {
                return new StringValue
                {
                    Value = v.Value + ((StringValue)rightV).Value
                };
            }

            return new BoolValue
            {
                Value = ((BoolValue)leftV).Value || ((BoolValue)rightV).Value
            };
        }
    }
}

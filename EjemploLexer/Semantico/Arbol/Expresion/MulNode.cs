﻿using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class MulNode: BinaryOperatorNode
    {
        public override Tipo EvaluateSemantic()
        {
            var ltipo = LeftOperand.EvaluateSemantic();
            var rtipo = RightOperand.EvaluateSemantic();
            if (ltipo.GetType() == rtipo.GetType())
            {
                if (ltipo is IntTipo || ltipo is BoolTipo)
                    return ltipo;

            }

           throw new SemanticException($"No se puede multiplicar entre tipo: {ltipo} y {rtipo}");
        }

        public override Value Interpret()
        {
            dynamic leftV = LeftOperand.Interpret();
            dynamic rightV = RightOperand.Interpret();
            return new IntValue { Value = leftV.Value * rightV.Value };
        }
    }
}
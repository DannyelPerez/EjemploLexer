﻿using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class SubNode: BinaryOperatorNode
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
            throw new SemanticException($"No se puede restar entre tipo: {ltipo} y {rtipo}");
        }

        public override Value Interpret()
        {
            dynamic leftV = LeftOperand.Interpret();
            dynamic rightV = RightOperand.Interpret();
            return new IntValue { Value = leftV.Value - rightV.Value };
        }
    }
}
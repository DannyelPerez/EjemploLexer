﻿using EjemploLexer.Semantico.Tipos;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class NumberLiteralNode: ExpressionNode
    {
        public int Value { get; set; }

        public override Tipo EvaluateSemantic()
        {
            return new IntTipo();
        }
    }
}
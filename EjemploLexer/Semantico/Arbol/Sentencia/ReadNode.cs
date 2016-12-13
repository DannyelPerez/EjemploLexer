using System;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Sintatico;

namespace EjemploLexer.Semantico.Arbol.Sentencia
{
    public class ReadNode: StatementNode
    {
        public IdNode Variable { get; set; }

        public override void ValidSemantic()
        {
            Variable.EvaluateSemantic();
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }
    }
}
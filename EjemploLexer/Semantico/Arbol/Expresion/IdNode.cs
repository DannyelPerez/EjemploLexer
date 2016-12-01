using System;
using EjemploLexer.Semantico.Tipos;
using EjemploLexer.Sintatico;

namespace EjemploLexer.Semantico.Arbol.Expresion
{
    public class IdNode: ExpressionNode
    {
        public string Name { get; set; }

        public override Tipo EvaluateSemantic()
        {
            if (!TablaSimbolos.Instance.VariableExist(Name))
                throw new SemanticException($"variable {Name} no existe");
            return TablaSimbolos.Instance.GetVariable(Name);
        }
    }

    public class SemanticException : Exception
    {
        public SemanticException(string msj) : base(msj) { }
    }
}
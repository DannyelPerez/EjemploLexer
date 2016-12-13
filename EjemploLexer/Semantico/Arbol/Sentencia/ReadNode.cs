using System;
using EjemploLexer.Interpretacion;
using EjemploLexer.Semantico.Arbol.Expresion;
using EjemploLexer.Semantico.Tipos;
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
            var value = Console.ReadLine();
            var variable = TablaSimbolos.Instance.GetVariable(Variable.Name);
            if (variable is StringTipo)
            {
                TablaSimbolos.Instance.SetVariableValue(Variable.Name,new StringValue {Value = value});
            }

            if (!(variable is IntTipo)) return;
            if (value != null)
                TablaSimbolos.Instance.SetVariableValue(Variable.Name, new IntValue { Value = int.Parse(value) });
        }
    }
}
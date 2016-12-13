using System.Collections.Generic;
using EjemploLexer.Interpretacion;

namespace EjemploLexer.Semantico.Tipos
{
    public class TablaSimbolos
    {
        private static TablaSimbolos _instance;
        private readonly Dictionary<string, Tipo> _variables;
        private readonly Dictionary<string, Value> _values;
        private TablaSimbolos()
        {
            _variables = new Dictionary<string, Tipo>();
            _values = new Dictionary<string, Value>();
        }

        public static TablaSimbolos Instance => _instance ?? (_instance = new TablaSimbolos());

        public void DeclareVariable(string name, Tipo type)
        {
            _variables.Add(name, type);
            _values.Add(name,type.GetDefaultValue());
        }

        public Tipo GetVariable(string name)
        {
            return _variables[name];
        }

        public void SetVariableValue(string name, Value value)
        {
            _values[name] = value;
        }

        public Value GetVariableValue(string name)
        {
            return _values[name];
        }

        public bool VariableExist(string name)
        {
            return _variables.ContainsKey(name);
        }


    }
}
using System.Collections.Generic;

namespace EjemploLexer.Semantico.Tipos
{
    public class TablaSimbolos
    {
        private static TablaSimbolos _instance = null;
        private Dictionary<string, Tipo> _variables;
        private TablaSimbolos()
        {
            _variables = new Dictionary<string, Tipo>();
        }

        public static TablaSimbolos Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TablaSimbolos();
                }
                return _instance;
            }
        }

        public void DeclareVariable(string name, Tipo type)
        {
            _variables.Add(name, type);
        }

        public Tipo GetVariable(string name)
        {
            return _variables[name];
        }

        public bool VariableExist(string name)
        {
            return _variables.ContainsKey(name);
        }


    }
}
using EjemploLexer.Interpretacion;

namespace EjemploLexer.Semantico.Tipos
{
    public class IntTipo: Tipo
    {
        public override string ToString()
        {
            return "Int";
        }

        public override Value GetDefaultValue()
        {
            return new IntValue {Value = 0};
        }
    }
}
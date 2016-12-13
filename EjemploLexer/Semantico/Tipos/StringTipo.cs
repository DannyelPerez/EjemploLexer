using EjemploLexer.Interpretacion;

namespace EjemploLexer.Semantico.Tipos
{
    public class StringTipo:Tipo
    {
        public override string ToString()
        {
            return "String";
        }

        public override Value GetDefaultValue()
        {
            return new StringValue {Value = ""};
        }
    }
}
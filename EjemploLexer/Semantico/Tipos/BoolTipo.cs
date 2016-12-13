using EjemploLexer.Interpretacion;

namespace EjemploLexer.Semantico.Tipos
{
    public class BoolTipo:Tipo
    {
        public override string ToString()
        {
            return "Bool";
        }

        public override Value GetDefaultValue()
        {
            return new BoolValue {Value = false};
        }
    }
}
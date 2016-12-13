using EjemploLexer.Interpretacion;

namespace EjemploLexer.Semantico.Tipos
{
    public abstract class Tipo
    {
        public abstract Value GetDefaultValue();
    }
}
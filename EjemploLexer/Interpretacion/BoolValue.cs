namespace EjemploLexer.Interpretacion
{
    public class BoolValue: Value
    {
        public bool Value { get; set; }
        public override Value Clone()
        {
            return new BoolValue { Value = this.Value };
        }
    }
}
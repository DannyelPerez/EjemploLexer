namespace EjemploLexer.Interpretacion
{
    public class StringValue: Value
    {
        public string Value { get; set; }
        public override Value Clone()
        {
            return new StringValue {Value = this.Value};
        }
    }
}
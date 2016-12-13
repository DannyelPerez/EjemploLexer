namespace EjemploLexer.Interpretacion
{
    public class IntValue: Value
    {
        public int Value { get; set; }
        public override Value Clone()
        {
            return  new IntValue {Value = this.Value};
        }
    }
}
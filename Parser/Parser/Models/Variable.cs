namespace Parser.Models
{
    public class Variable:Symbol
    {
        public Variable()
        {
            SymbolType = SymbolType.Variable;
        }
        public Variable(string value):this()
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Variable var)
            {
                return var.Value == Value;
            }

            return false;
        }

        protected bool Equals(Variable other)
        {
            return other.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

using System.CodeDom;

namespace Parser.Models
{
    public class Terminal:Symbol
    {
        public Terminal(string value)
        {
            Value = value;
            SymbolType = SymbolType.Terminal;
        }
        public string Value { get; set; }
        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Terminal term)
            {
                return term.Value == Value;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

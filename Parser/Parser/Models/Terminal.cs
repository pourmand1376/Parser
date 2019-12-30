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
        
        public override bool Equals(object obj)
        {
            if (obj is Terminal term)
            {
                return term.Value == Value;
            }

            return false;
        }

        protected bool Equals(Terminal other)
        {
            return other.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

using System.CodeDom;
using System.Data;

namespace Parser.Models
{
    public class Terminal:ISymbol
    {
        public static Terminal EndOfFile { get; }=new Terminal( "$");

        public static Terminal Epsilon { get; }= new Terminal("ε");

        public SymbolType SymbolType { get; }

        public string Value { get; }

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

        public override string ToString()
        {
            return $" \"{Value}\" ";
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Parser.Models
{
    public class Variable:ISymbol
    {
        public SymbolType SymbolType { get; }

        public string Value { get; }

        public List<IEnumerable<ISymbol>> Definitions { get; set; }

        public Variable(string value)
        {
            SymbolType = SymbolType.Variable;
            Value = value;
            Definitions = new List<IEnumerable<ISymbol>>();
        }

        public string ShowRules()
        {
            return $"{Value} ==> " +
                   string.Join(" | ", (
                       from ruleSet in Definitions
                       select string.Join("",ruleSet)));
        }
        public override string ToString()
        {
            return $" <{Value}> ";
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

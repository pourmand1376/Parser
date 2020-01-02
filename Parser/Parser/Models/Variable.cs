using System.Collections.Generic;
using System.Linq;

namespace Parser.Models
{
    public class RuleSet
    {
        public RuleSet(Variable variable)
        {
            Variable = variable;
            Definitions = new List<IEnumerable<ISymbol>>();
        }

        public List<IEnumerable<ISymbol>> Definitions { get; set; }

        public Variable Variable { get; }
    }

    public class Variable:ISymbol
    {
        public SymbolType SymbolType { get; }

        public string Value { get; }

        public List<Terminal> Firsts { get; set; }
        public List<Terminal> Follows { get; set; }

        public bool IsCalculatingFirst { get; set; }
        public bool IsCalculatingFollow { get; set; }
        
        public bool FirstReady { get; set; }
        //Can't check if follow is null because $ should be added first
        public bool FollowReady { get; set; }

        public RuleSet RuleSet { get; }

        public Variable(string value)
        {
            SymbolType = SymbolType.Variable;
            Value = value;
            Firsts = new List<Terminal>();
            Follows = new List<Terminal>();
            FirstReady = false;
            FollowReady = false;
            RuleSet = new RuleSet(this);
        }

        public string ShowRules()
        {
            return $"{this} ==> " +
                   string.Join(" | ", (
                       from ruleSet in RuleSet.Definitions
                       select string.Join("",ruleSet)));
        }

        public string ShowFirsts()
        {
            return $"{this} ==> " + string.Join(", ", Firsts);
        }

        public string ShowFollows()
        {
            return $"{this} ==> " + string.Join(", ", Follows);
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

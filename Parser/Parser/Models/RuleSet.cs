using System.Collections.Generic;

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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser.Lexical
{
   public class GrammarADT
    {
        public Dictionary<Variable, List<RightHandSide>> GrammerRules { get; set; }

        public Dictionary<Symbol,HashSet<Terminal>> FirstSet { get; set; }

        public Dictionary<Symbol,HashSet<Terminal>> FollowSet { get; set; }

        public Variable HeadVariable { get; set; }

        public GrammarADT()
        {
            GrammerRules = new Dictionary<Variable, List<RightHandSide>>();
            FirstSet = new Dictionary<Symbol, HashSet<Terminal>>();
            FollowSet = new Dictionary<Symbol, HashSet<Terminal>>();
        }

        public bool HasEmptyRule(Variable variable)
        {
            var producedRules = GrammerRules[variable];
            foreach (var producedRule in producedRules
                .Where(producedRule => producedRule.SymbolList.Count == 0))
            {
                return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser.Lexical;
using Parser.Models;

namespace Parser
{
    
    public class LexicalAnalyzer
    {
        // Variable -> ProducedRule 
        // Produced Rule is a list of variable or terminals

        private readonly GrammarRules _grammarRules;

        private string Data { get; set; }

        public LexicalAnalyzer(string data)
        {
            Data = data;
            _grammarRules = new GrammarRules();
        }

        public GrammarRules TokenizeGrammar()
        {
            var lines = Data.Split('\n');
            foreach (var line in lines)
            {
                if(!string.IsNullOrWhiteSpace(line))
                    LineTokenExtractor(line);                
            }

            return _grammarRules;
        }

        public List<Terminal> TokenizeInputText()
        {
            var lines = Data.Split('\n');

            return (from line in lines
                .Where(s => !string.IsNullOrEmpty(s)) 
                from item in line.Split(' ') 
                select new Terminal(item)).ToList();
        }

        private void LineTokenExtractor(string line)
        {
            Regex text = new Regex(@"<(?<variable>[\w-]+)>|""(?<terminal>[^""<>:]+)?""",RegexOptions.Compiled);

            var matches=text.Matches(line);
            var firstVariable = matches[0].Groups["variable"];

            if (!firstVariable.Success) return;
            var headVariable=_grammarRules.GetOrCreateSymbol(firstVariable.Value, SymbolType.Variable);

            var symbols = new List<ISymbol>();
            for (var index = 1; index < matches.Count; index++)
            {
                Match match = matches[index];
                var variable = match.Groups["variable"];
                if (variable.Success)
                {
                    symbols.Add(_grammarRules.GetOrCreateSymbol(variable.Value,SymbolType.Variable));
                    continue;
                }

                var terminal = match.Groups["terminal"];
                if (terminal.Success)
                {
                    symbols.Add(_grammarRules.GetOrCreateSymbol(terminal.Value,SymbolType.Terminal));
                    continue;
                }

                //if it comes here then it's epsilon
                symbols.Add(_grammarRules.GetOrCreateSymbol("",SymbolType.Terminal));
            }


            if (_grammarRules.HeadVariable == null)
            {
                _grammarRules.HeadVariable = (Variable)headVariable;
            }
            ((Variable)headVariable).RuleSet.Definitions.Add(symbols);
        }

    }
}

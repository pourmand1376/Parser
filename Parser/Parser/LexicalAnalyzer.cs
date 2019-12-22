using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser
{
    public class LexicalAnalyzer
    {
        private Dictionary<Variable, List<ProducedRule>> _grammerRules;

        public string Data { get; set; }

        public LexicalAnalyzer(string data)
        {
            _grammerRules = new Dictionary<Variable, List<ProducedRule>>();
            Data = data;
        }

        public void Tokenize()
        {
            var lines = Data.Split('\n');
            foreach (string line in lines)
            {
                if(!string.IsNullOrWhiteSpace(line))
                    LineTokenExtractor(line);                
            }
        }

        private void LineTokenExtractor(string line)
        {
            Regex text = new Regex(@"<(?<variable>[\w-]+)>|\""(?<terminal>[\w-.,]+)?\""",RegexOptions.Compiled);

            Variable head = new Variable();
            ProducedRule rule = new ProducedRule(){ProducedItems = new List<Base>()};


            var matches=text.Matches(line);
            
            foreach (Match match in matches)
            {
                var variable = match.Groups["variable"];
                if (variable.Success)
                {
                    if (head.Name==null) //first Variable
                    {
                        head.Name = variable.Value;
                        //add list of production rules if there is more than one production rule
                        if(!_grammerRules.ContainsKey(head))
                            _grammerRules.Add(head,new List<ProducedRule>());
                    }
                    else
                    {
                        rule.ProducedItems.Add(new Variable(variable.Value));
                    }
                }
                var terminal = match.Groups["terminal"];
                if (terminal.Success)
                {
                    rule.ProducedItems.Add(new Terminal(terminal.Value));
                }
            }

            var productionRules = _grammerRules[head];
            productionRules.Add(rule);
        }

    }
}

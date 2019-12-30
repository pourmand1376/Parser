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

        public GrammarADT GrammarAdt { get;  }

        public string Data { get; set; }

        public LexicalAnalyzer(string data)
        {
            Data = data;
            GrammarAdt = new GrammarADT();
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
            Regex text = new Regex(@"<(?<variable>[\w-]+)>|""(?<terminal>[^""<>:]+)?""",RegexOptions.Compiled);

            Variable head = new Variable();
            RightHandSide rule = new RightHandSide(){SymbolList = new List<Symbol>()};

            
            var matches=text.Matches(line);
            
            foreach (Match match in matches)
            {
                var variable = match.Groups["variable"];
                if (variable.Success)
                {
                    if (head.Value==null) //first Variable
                    {
                        head.Value = variable.Value;
                        //add list of production rules if there is more than one production rule
                        if(!GrammarAdt.GrammerRules.ContainsKey(head)) 
                            GrammarAdt.GrammerRules.Add(head,new List<RightHandSide>());
                    }
                    else
                    {
                        rule.SymbolList.Add(new Variable(variable.Value));
                    }
                    continue;
                }
                var terminal = match.Groups["terminal"];
                if (terminal.Success)
                {
                    rule.SymbolList.Add(new Terminal(terminal.Value));
                    continue;
                }

                //if it comes here then it's epsilon
                rule.SymbolList.Add(new Terminal(ConstValues.Epsilon));
            }
            

            if (GrammarAdt.HeadVariable == null)
            {
                GrammarAdt.HeadVariable = head;
            }

            var productionRules = GrammarAdt.GrammerRules[head];
            productionRules.Add(rule);
        }

    }
}

using System;
using System.Collections.Generic;
using Parser.Lexical;
using Parser.Models;

namespace Parser.Parse
{
    public class Preprocessor
    {
        private GrammarADT _grammarAdt { get; }

        public Preprocessor(GrammarADT grammarAdt)
        {
            _grammarAdt = grammarAdt;
        }
        
        
        public void FirstTerminals(Symbol symbol)
        {
            if (_grammarAdt.FirstSet.ContainsKey(symbol))
            {
                return;
            }
            HashSet<Terminal> firstSet = new HashSet<Terminal>();
            if (symbol is Terminal term)
            {
                firstSet.Add(term);
                _grammarAdt.FirstSet.Add(symbol,firstSet);
                return;
            }
            
            var baseVariable = symbol as Variable;
            if (_grammarAdt.HasEmptyRule(baseVariable))
            {
                firstSet.Add(new Terminal(""));
            }
            //submit to dictionary till now
            _grammarAdt.FirstSet.Add(symbol,firstSet);

            var grammars = _grammarAdt.GrammerRules[baseVariable];

            foreach (RightHandSide rightHandSide in grammars)
            {
                for (int i = 0; i < rightHandSide.SymbolList.Count; i++)
                {
                    if (rightHandSide.SymbolList[i] is Terminal terminal)
                    {
                        _grammarAdt.FirstSet[symbol].Add(terminal);
                        break;
                    }

                    var variable = rightHandSide.SymbolList[i] as Variable;
                    //Calculate First Terminal
                    FirstTerminals(variable);
                    foreach (Terminal terminalOfVariable in _grammarAdt.FirstSet[variable])
                    {
                        _grammarAdt.FirstSet[symbol].Add(terminalOfVariable);
                    }

                    if (!_grammarAdt.HasEmptyRule(variable))
                    {
                        break;
                    }
                }
            }
            
        }
    }
}

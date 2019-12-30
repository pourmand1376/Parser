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
            if (symbol.SymbolType==SymbolType.Terminal)
            {
                firstSet.Add(symbol.GetTerminal());
                _grammarAdt.FirstSet.Add(symbol,firstSet);
                return;
            }
            
            var baseVariable = symbol.GetVariable();
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
                    var rightHandSideValue = rightHandSide.SymbolList[i];
                    if (rightHandSideValue.SymbolType==SymbolType.Terminal)
                    { 
                        _grammarAdt.FirstSet[symbol].Add(rightHandSideValue.GetTerminal());
                        break;
                    }

                    var variable = rightHandSide.SymbolList[i].GetVariable();
                    //Calculate First Terminal
                    FirstTerminals(variable);

                    Terminal[] terminals= new Terminal[_grammarAdt.FirstSet[variable].Count];
                     _grammarAdt.FirstSet[variable].CopyTo(terminals);
                    foreach (Terminal terminalOfVariable in terminals)
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

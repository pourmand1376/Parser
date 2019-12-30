using Parser.Lexical;
using Parser.Models;
using System.Collections.Generic;

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
            if ( _grammarAdt.FirstSet.ContainsKey(symbol) )
            {
                return;
            }
            HashSet<Terminal> firstSet = new HashSet<Terminal>();
            if ( symbol.SymbolType == SymbolType.Terminal )
            {
                firstSet.Add(symbol.GetTerminal());
                _grammarAdt.FirstSet.Add(symbol,firstSet);
                return;
            }

            var baseVariable = symbol.GetVariable();
            if ( _grammarAdt.HasEmptyRule(baseVariable) )
            {
                firstSet.Add(new Terminal(ConstValues.Epsilon));
            }
            //submit to dictionary till now
            _grammarAdt.FirstSet.Add(symbol,firstSet);

            var grammars = _grammarAdt.GrammerRules [baseVariable];

            foreach ( RightHandSide rightHandSide in grammars )
            {
                for ( int i = 0 ;i < rightHandSide.SymbolList.Count ;i++ )
                {
                    var rightHandSideValue = rightHandSide.SymbolList [i];
                    if ( rightHandSideValue.SymbolType == SymbolType.Terminal )
                    {
                        _grammarAdt.FirstSet [symbol].Add(rightHandSideValue.GetTerminal());
                        break;
                    }

                    var variable = rightHandSide.SymbolList [i].GetVariable();
                    //Calculate First Terminal
                    FirstTerminals(variable);

                    Terminal [] terminals = new Terminal [_grammarAdt.FirstSet [variable].Count];
                    _grammarAdt.FirstSet [variable].CopyTo(terminals);
                    foreach ( Terminal terminalOfVariable in terminals )
                    {
                        _grammarAdt.FirstSet [symbol].Add(terminalOfVariable);
                    }

                    if ( !_grammarAdt.HasEmptyRule(variable) )
                    {
                        break;
                    }
                }
            }

        }

        public void FollowTerminals(Symbol symbol)
        {
            var hashSet = new HashSet<Terminal>();
            _grammarAdt.FollowSet.Add(symbol,hashSet);
            foreach ( KeyValuePair<Variable,List<RightHandSide>> grammarAdtGrammerRule in _grammarAdt.GrammerRules )
            {
                foreach ( RightHandSide rightHandSide in grammarAdtGrammerRule.Value )
                {
                    bool found = false;
                    for (var index = 0; index < rightHandSide.SymbolList.Count; index++)
                    {
                        Symbol currentSymbol = rightHandSide.SymbolList[index];
                        if (found)
                        {
                            if (symbol.SymbolType == SymbolType.Terminal)
                            {
                                hashSet.Add(currentSymbol.GetTerminal());
                                break;
                            }

                            foreach (Terminal terminal in _grammarAdt.FirstSet[currentSymbol])
                            {
                                hashSet.Add(terminal);
                            }

                            if (!_grammarAdt.FirstSet[currentSymbol].Contains(new Terminal(ConstValues.Epsilon))) break;
                        }
                        else if (currentSymbol == symbol)
                        {
                            found = true;
                        }

                        // اگر found درست باشد باید بعدی بررسی شود اما اگر بعدی ای وجود نداشت باید 
                        //follow سرمتغیر را اضافه کنیم!
                        if (found && index + 1 >= rightHandSide.SymbolList.Count)
                        {
                            FollowTerminals(grammarAdtGrammerRule.Key);
                            foreach (Terminal terminal in _grammarAdt.FollowSet[grammarAdtGrammerRule.Key])
                            {
                                hashSet.Add(terminal);
                            }
                        }


                    }
                }
            }
        }
    }
}

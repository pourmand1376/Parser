using Parser.Lexical;
using Parser.Models;
using System.Collections.Generic;

namespace Parser.Parse
{
    
    public class Preprocessor
    {

        private GrammarRules GrammarRules { get; }

        public Preprocessor(GrammarRules grammarRules)
        {
            GrammarRules = grammarRules;
        }

        /*
        public void FirstTerminals(ISymbol symbol)
        {
            if ( GrammarRules.FirstSet.ContainsKey(symbol) )
            {
                return;
            }
            HashSet<Terminal> firstSet = new HashSet<Terminal>();
            if ( symbol.SymbolType == SymbolType.Terminal )
            {
                firstSet.Add(symbol.GetTerminal());
                GrammarRules.FirstSet.Add(symbol,firstSet);
                return;
            }

            var baseVariable = symbol.GetVariable();
            if ( GrammarRules.HasEmptyRule(baseVariable) )
            {
                firstSet.Add(new Terminal(ConstValues.Epsilon));
            }
            //submit to dictionary till now
            GrammarRules.FirstSet.Add(symbol,firstSet);

            var grammars = GrammarRules.GrammerRules [baseVariable];

            foreach ( RightHandSide rightHandSide in grammars )
            {
                for ( int i = 0 ;i < rightHandSide.SymbolList.Count ;i++ )
                {
                    var rightHandSideValue = rightHandSide.SymbolList [i];
                    if ( rightHandSideValue.SymbolType == SymbolType.Terminal )
                    {
                        GrammarRules.FirstSet [symbol].Add(rightHandSideValue.GetTerminal());
                        break;
                    }

                    var variable = rightHandSide.SymbolList [i].GetVariable();
                    //Calculate First Terminal
                    FirstTerminals(variable);

                    Terminal [] terminals = new Terminal [GrammarRules.FirstSet [variable].Count];
                    GrammarRules.FirstSet [variable].CopyTo(terminals);
                    foreach ( Terminal terminalOfVariable in terminals )
                    {
                        GrammarRules.FirstSet [symbol].Add(terminalOfVariable);
                    }

                    if ( !GrammarRules.HasEmptyRule(variable) )
                    {
                        break;
                    }
                }
            }

        }

        public void FollowTerminals(ISymbol symbol)
        {
            var hashSet = new HashSet<Terminal>();
            GrammarRules.FollowSet.Add(symbol,hashSet);
            foreach ( KeyValuePair<Variable,List<RightHandSide>> grammarAdtGrammerRule in GrammarRules.GrammerRules )
            {
                foreach ( RightHandSide rightHandSide in grammarAdtGrammerRule.Value )
                {
                    bool found = false;
                    for (var index = 0; index < rightHandSide.SymbolList.Count; index++)
                    {
                        ISymbol currentSymbol = rightHandSide.SymbolList[index];
                        if (found)
                        {
                            if (symbol.SymbolType == SymbolType.Terminal)
                            {
                                hashSet.Add(currentSymbol.GetTerminal());
                                break;
                            }

                            foreach (Terminal terminal in GrammarRules.FirstSet[currentSymbol])
                            {
                                hashSet.Add(terminal);
                            }

                            if (!GrammarRules.FirstSet[currentSymbol].Contains(new Terminal(ConstValues.Epsilon))) break;
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
                            foreach (Terminal terminal in GrammarRules.FollowSet[grammarAdtGrammerRule.Key])
                            {
                                hashSet.Add(terminal);
                            }
                        }


                    }
                }
            }
        }*/
    }
}

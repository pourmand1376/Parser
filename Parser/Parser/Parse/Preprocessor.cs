using Parser.Lexical;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;

namespace Parser.Parse
{
    public class Preprocessor
    {
        private GrammarRules GrammarRules { get; }

        public Preprocessor(GrammarRules grammarRules)
        {
            GrammarRules = grammarRules;
        }

        public void CalculateAllFirsts()
        {
            foreach (ISymbol symbolsValue in GrammarRules.Symbols.Values)
            {
                if (symbolsValue is Variable variable)
                {
                    if (!variable.FirstReady)
                    {
                        variable.IsCalculatingFirst = true;
                        variable.Firsts = FirstSet(variable.Definitions);
                        variable.IsCalculatingFirst = false;
                    }

                }
            }
        }

        public void CalculateFollowSets()
        {
            GrammarRules.HeadVariable.Follows.Add(Terminal.EndOfFile);
            foreach (ISymbol symbolsValue in GrammarRules.Symbols.Values)
            {
                if (symbolsValue is Variable variable)
                {
                    if (!variable.FollowReady)
                    {
                        variable.Follows = FollowSets(variable);
                    }
                }
            }
        }

        private List<Terminal> FirstSet(List<IEnumerable<ISymbol>> rules)
        {
            return rules.SelectMany(rule =>
                {
                    var terminals = new List<Terminal>();
                    bool canBeEmpty = true;
                    foreach (ISymbol symbol in rule)
                    {
                        if (symbol is Terminal terminal && 
                            !symbol.Equals(Terminal.Epsilon))
                        {
                            terminals.Add((Terminal)GrammarRules.Symbols[terminal.Value]);
                            canBeEmpty = false;
                            break;
                        }

                        if (symbol is Variable variable)
                        {
                            //preventing the stackoverflow exception
                            if (variable.IsCalculatingFirst)
                                return new List<Terminal>();

                            if (!variable.FirstReady)
                            {
                                variable.IsCalculatingFirst = true;
                                var first = FirstSet(variable.Definitions);
                                variable.IsCalculatingFirst = false;
                                variable.Firsts = first;
                                variable.FirstReady = true;
                            }

                            var firsts = variable.Firsts;
                            
                            if (!firsts.Contains(Terminal.Epsilon))
                            {
                                canBeEmpty = false;
                                terminals.AddRange(firsts);
                                break;
                            }
                            else 
                                terminals.AddRange(firsts
                                    .Where(term=>!term.Equals(Terminal.Epsilon)));
                        }
                    }
                    if(canBeEmpty) terminals.Add(Terminal.Epsilon);
                    return terminals;
                })
                .Distinct().ToList();
        }

        private List<Terminal> FollowSets(Variable variable)
        {
            
            var result= GrammarRules.Symbols.Values.SelectMany(symbol =>
            {
                if (!(symbol is Variable currentVar)) return new List<Terminal>();
                List<Terminal> follow = new List<Terminal>();
                foreach (IEnumerable<ISymbol> currentRule in currentVar.Definitions
                    .Where(rule => rule.Contains(variable)))
                {
                    
                    var tempCurrentRule = currentRule;
                    //if it still contains that variable
                    while (tempCurrentRule.Contains(variable))
                    {
                        //go forward till you arrive to the point the variable exists
                        tempCurrentRule = tempCurrentRule.SkipWhile(s => !s.Equals(variable)).Skip(1);
                        //is it's normal!
                        if (tempCurrentRule.Any())
                        {
                            var firsts = FirstSet(new List<IEnumerable<ISymbol>>() {tempCurrentRule});
                            follow.AddRange(firsts.Where(t => !t.Equals(Terminal.Epsilon)));

                            if (firsts.Contains(Terminal.Epsilon))
                            {
                                if (currentVar.FollowReady)
                                {
                                    var follows = FollowSets(currentVar);
                                    currentVar.Follows.AddRange(follows);
                                    currentVar.FollowReady = true;
                                }

                                follow.AddRange(currentVar.Follows);
                            }
                        }
                        // <D> ::= "d" <D>
                        else if (!currentVar.Equals(variable))
                        {
                            if (!currentVar.FollowReady)
                            {
                                var follows = FollowSets(currentVar);
                                currentVar.Follows.AddRange(follows);
                                currentVar.FollowReady = true;
                            }

                            follow.AddRange(currentVar.Follows);
                        }
                    }
                }
                return follow;
            }).Distinct().ToList();
            
            return result;
        }
    }
}

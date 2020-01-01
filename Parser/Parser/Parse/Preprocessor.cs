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
            foreach ( ISymbol symbolsValue in GrammarRules.Symbols.Values )
            {
                if ( symbolsValue is Variable variable )
                {
                    if ( !variable.FirstReady )
                    {
                        variable.IsCalculatingFirst = true;
                        variable.Firsts.AddRange(FirstSet(variable.Definitions));
                        variable.IsCalculatingFirst = false;
                        variable.FirstReady = true;
                        variable.Firsts = variable.Firsts.Distinct().ToList();
                    }

                }
            }
            
        }

        public void CalculateAllFollows()
        {
            GrammarRules.HeadVariable.Follows.Add(Terminal.EndOfFile);
            CalculateFollowSets();
            ClearAllFollowReady();
            CalculateFollowSets();
        }

        private void ClearAllFollowReady()
        {
            foreach ( ISymbol symbolsValue in GrammarRules.Symbols.Values )
            {
                if ( symbolsValue is Variable variable )
                { 
                    variable.FollowReady = false;   
                }
            }
        }
        private void CalculateFollowSets()
        {
            foreach ( ISymbol symbolsValue in GrammarRules.Symbols.Values )
            {
                if ( symbolsValue is Variable variable )
                {
                    if ( !variable.FollowReady )
                    {
                        variable.Follows.AddRange(FollowSets(variable));
                        variable.FollowReady = true;
                        variable.Follows = variable.Follows.Distinct().ToList();
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
                    foreach ( ISymbol symbol in rule )
                    {
                        if ( symbol is Terminal terminal &&
                            !symbol.Equals(Terminal.Epsilon) )
                        {
                            terminals.Add((Terminal) GrammarRules.Symbols [terminal.Value]);
                            canBeEmpty = false;
                            break;
                        }

                        if ( symbol is Variable variable )
                        {
                            //preventing the stackoverflow exception
                            if ( variable.IsCalculatingFirst )
                                return new List<Terminal>();

                            if ( !variable.FirstReady )
                            {
                                variable.IsCalculatingFirst = true;
                                var first = FirstSet(variable.Definitions);
                                variable.IsCalculatingFirst = false;
                                variable.Firsts.AddRange(first);
                                variable.FirstReady = true;
                            }

                            var firsts = variable.Firsts;

                            if ( !firsts.Contains(Terminal.Epsilon) )
                            {
                                canBeEmpty = false;
                                terminals.AddRange(firsts);
                                break;
                            }
                            else
                                terminals.AddRange(firsts
                                    .Where(term => !term.Equals(Terminal.Epsilon)));
                        }
                    }
                    if ( canBeEmpty ) terminals.Add(Terminal.Epsilon);
                    return terminals;
                })
                .Distinct().ToList();
        }

        public List<Terminal> FollowSets(Variable variable)
        {
            if (variable.IsCalculatingFollow)
                return variable.Follows;

            variable.IsCalculatingFollow = true;
            var result = GrammarRules.Symbols.Values.SelectMany(symbol =>
             {
                 if ( !( symbol is Variable currentVar ) ) return new List<Terminal>();
                 List<Terminal> follow = new List<Terminal>();
                 foreach ( IEnumerable<ISymbol> currentRule in currentVar.Definitions
                     .Where(rule => rule.Contains(variable)) )
                 {

                     var tempCurrentRule = currentRule;

                     while ( tempCurrentRule.Contains(variable) )
                     {
                         tempCurrentRule = tempCurrentRule.SkipWhile(s => !s.Equals(variable)).Skip(1);
                         if ( tempCurrentRule.Any() )
                         {
                             var firsts = FirstSet(new List<IEnumerable<ISymbol>>() { tempCurrentRule });
                             follow.AddRange(firsts.Where(t => !t.Equals(Terminal.Epsilon)));
                             if ( firsts.Contains(Terminal.Epsilon) )
                             {
                                 if ( !currentVar.FollowReady )
                                 {
                                     var follows = FollowSets(currentVar);
                                     currentVar.Follows.AddRange(follows);
                                     currentVar.FollowReady = true;
                                 }

                                 follow.AddRange(currentVar.Follows);
                             }
                         }
                         else if ( !currentVar.Equals(variable) )
                         {
                             if ( !currentVar.FollowReady )
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
            variable.IsCalculatingFollow = false;
            return result;
        }
    }
}

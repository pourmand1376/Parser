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
            foreach ( ISymbol symbolsValue in GrammarRules.SymbolList )
            {
                if ( symbolsValue is Variable variable )
                {
                    if ( !variable.FirstReady )
                    {
                        variable.IsCalculatingFirst = true;
                        variable.Firsts.AddRange(FirstSet(variable.Definitions));
                        variable.IsCalculatingFirst = false;
                        variable.FirstReady = true;
                    }

                }
            }
            RemoveDuplicates();
        }
        
        public void CalculateAllFollows()
        {
            GrammarRules.HeadVariable.Follows.Add(Terminal.EndOfFile);
            CalculateFollowSets();
            ClearAllFollowReady();
            CalculateFollowSets();
            RemoveDuplicates();
        }

        private void ClearAllFollowReady()
        {
            foreach ( ISymbol symbolsValue in GrammarRules.SymbolList )
            {
                if ( symbolsValue is Variable variable )
                { 
                    variable.FollowReady = false;   
                }
            }
        }

        private void RemoveDuplicates()
        {
            foreach ( ISymbol symbolsValue in GrammarRules.SymbolList)
            {
                if ( symbolsValue is Variable variable )
                {
                    variable.Follows = variable.Follows.Distinct().ToList();
                    variable.Firsts = variable.Firsts.Distinct().ToList();
                }
            }
        }
        private void CalculateFollowSets()
        {
            foreach ( ISymbol symbolsValue in GrammarRules.SymbolList)
            {
                if ( symbolsValue is Variable variable )
                {
                    if ( !variable.FollowReady )
                    {
                        variable.Follows.AddRange(FollowSets(variable));
                        variable.FollowReady = true;
                    }
                }
            }
        }

        public List<Terminal> FirstSet(List<IEnumerable<ISymbol>> rules)
        {
            return rules.SelectMany(rule =>
                {
                    var terminals = new List<Terminal>();
                    bool canBeEmpty = true;
                    foreach ( ISymbol symbol in rule )
                    {
                        if ( symbol is Terminal terminal 
                             && !symbol.Equals(Terminal.Epsilon)
                             )
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
                            //else
                                terminals.AddRange(firsts
                                    .Where(term => !term.Equals(Terminal.Epsilon)));
                        }
                    }
                    //if you couldn't all any terminal at this rule
                    if ( canBeEmpty ) terminals.Add(Terminal.Epsilon);
                    return terminals;
                })
                .Distinct().ToList();
        }

        public List<Terminal> FollowSets(Variable variable)
        {
            //preventing stackoverflow exception
            if (variable.IsCalculatingFollow)
                return variable.Follows;

            variable.IsCalculatingFollow = true;
            var result = GrammarRules.SymbolList.SelectMany(symbol =>
             {
                 //if it's terminal do nothing
                 if ( !( symbol is Variable currentVar ) ) return new List<Terminal>();

                 List<Terminal> follow = new List<Terminal>();
                 //go over all rules that contain the grammar
                 foreach ( IEnumerable<ISymbol> currentRule in currentVar.Definitions
                     .Where(rule => rule.Contains(variable)) )
                 {

                     var tempCurrentRule = currentRule;
                     //while you have it
                     while ( tempCurrentRule.Contains(variable) )
                     {
                         //skip till you get to it
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
                         //<D> ::= "a"<D>
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

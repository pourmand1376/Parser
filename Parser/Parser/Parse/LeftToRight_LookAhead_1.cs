using System.Collections.Generic;
using System.Linq;
using Parser.Lexical;
using Parser.Models;

namespace Parser.Parse
{
    //LL(1)
    public class LeftToRight_LookAhead_1
    {
        private readonly GrammarRules _grammarRules;
        private IEnumerable<ISymbol>[,] table;

        public Dictionary<string, int> MapVariableToNumber { get; set; }
        public Dictionary<string, int> MapTerminalToNumber { get; set; }

        public int VariableCount { get; set; }
        public int TerminalCount { get; set; }
        public LeftToRight_LookAhead_1(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            MapTerminalToNumber = new Dictionary<string, int>();
            MapVariableToNumber = new Dictionary<string, int>();
        }

        public void Init()
        {
             VariableCount = 0;
             TerminalCount = 0;
            foreach (var symbol in _grammarRules.Symbols.Values
                .Where(symbol => !symbol.Equals(Terminal.Epsilon) && 
                                 !symbol.Equals(Terminal.EndOfFile)))
            {
                if (symbol.SymbolType == SymbolType.Variable)
                {
                    MapVariableToNumber.Add(symbol.Value,VariableCount);
                    VariableCount++;
                }

                else
                {
                    MapTerminalToNumber.Add(symbol.Value,TerminalCount);
                    TerminalCount++;
                }
            }
            MapTerminalToNumber.Add(Terminal.EndOfFile.Value,TerminalCount++);
            table = new IEnumerable<ISymbol>[VariableCount,TerminalCount];
        }

        public IEnumerable<ISymbol>[,] ProcessTable()
        {
            Preprocessor preprocessor = new Preprocessor(_grammarRules);
            foreach (ISymbol symbolsValue in _grammarRules.Symbols.Values)
            {
                if (symbolsValue is Variable variable)
                {
                    var variableNumber = MapVariableToNumber[variable.Value];
                    foreach (IEnumerable<ISymbol> variableDefinition in variable.Definitions)
                    {
                        var firsts=preprocessor.FirstSet(new List<IEnumerable<ISymbol>>() {variableDefinition});
                        
                        foreach (Terminal terminal in firsts.Where(term=>!term.Equals(Terminal.Epsilon)))
                        {
                            table[variableNumber, MapTerminalToNumber[terminal.Value]] = variableDefinition;
                        }

                        if (firsts.Contains(Terminal.Epsilon))
                        {
                            var variableFollows = variable.Follows;
                            foreach (Terminal variableFollow in variableFollows)
                            {
                                table[variableNumber, MapTerminalToNumber[variableFollow.Value]] = new[] {Terminal.Epsilon, };
                            }
                        }
                    }
                }
            }

            return table;
        }
        
    }
}

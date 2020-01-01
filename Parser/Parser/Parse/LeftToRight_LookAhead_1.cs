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
        private int[,] table;

        private Dictionary<string, int> MapVariableToNumber;
        private Dictionary<string, int> MapTerminalToNumber;
        public LeftToRight_LookAhead_1(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            MapTerminalToNumber = new Dictionary<string, int>();
            MapVariableToNumber = new Dictionary<string, int>();
        }

        public void Init()
        {
            int variableCount = 0;
            int terminalCount = 0;
            foreach (var symbol in _grammarRules.Symbols.Values
                .Where(symbol => !symbol.Equals(Terminal.Epsilon) && 
                                 !symbol.Equals(Terminal.EndOfFile)))
            {
                if (symbol.SymbolType == SymbolType.Variable)
                {
                    MapVariableToNumber.Add(symbol.Value,variableCount);
                    variableCount++;
                }

                else
                {
                    MapTerminalToNumber.Add(symbol.Value,terminalCount);
                    terminalCount++;
                }
            }
            table = new int[variableCount,terminalCount];
        }

        public int[,] ProcessTable()
        {
            foreach (ISymbol symbolsValue in _grammarRules.Symbols.Values)
            {
                if (symbolsValue is Variable variable)
                {
                    var number = MapVariableToNumber[variable.Value];
                    
                }
            }
        }
        
    }
}

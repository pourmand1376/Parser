using System.Collections.Generic;
using System.Linq;
using Parser.Lexical;
using Parser.Models;

namespace Parser.Parse
{
    /// <summary>
    /// Maps Variable and states to number
    /// for example S -> 2
    /// 
    /// </summary>
    public class MapperToNumber
    {
        private readonly GrammarRules _grammarRules;

        /// <summary>
        /// variable mapping
        /// </summary>
        public Dictionary<string, int> MapVariableToNumber { get; }

        /// <summary>
        /// terminal mapping
        /// </summary>
        public Dictionary<string, int> MapTerminalToNumber { get; }

        /// <summary>
        /// count of Variables
        /// </summary>
        public int VariableCount { get; private set; }

        /// <summary>
        /// count of terminals
        /// </summary>
        public int TerminalCount { get; private set; }

        public MapperToNumber(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            MapTerminalToNumber = new Dictionary<string, int>();
            MapVariableToNumber = new Dictionary<string, int>();
        }

        /// <summary>
        /// calculating terminal and variables count
        /// note that I put $ in the terminals
        /// </summary>
        public void Initialize()
        {
            VariableCount = 0; 
            TerminalCount = 0;
            foreach (var symbol in _grammarRules.SymbolList
                .Where(symbol => !symbol.Equals(Terminal.Epsilon) && 
                                 !symbol.Equals(Terminal.EndOfFile)))
            {
                if (symbol.SymbolType == SymbolType.Variable)
                {
                    MapVariableToNumber.Add(symbol.Value, VariableCount);
                    VariableCount++;
                }
                else
                {
                    MapTerminalToNumber.Add(symbol.Value, TerminalCount);
                    TerminalCount++;
                }
            }
            MapTerminalToNumber.Add(Terminal.EndOfFile.Value, TerminalCount++);
        }
    }
}
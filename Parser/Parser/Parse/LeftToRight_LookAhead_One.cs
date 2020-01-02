using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Parser.Lexical;
using Parser.Models;

namespace Parser.Parse
{
    //LL(1)

    public class LeftToRight_LookAhead_One
    {
        private readonly GrammarRules _grammarRules;
        private readonly IProgress<ParseReportModel> _progress;
        private List<ISymbol>[,] table;
        private readonly MapperToNumber _mapperToNumber;

        public LeftToRight_LookAhead_One(GrammarRules grammarRules,IProgress<ParseReportModel> progress)
        {
            
            _grammarRules = grammarRules;
            _progress = progress;
            _mapperToNumber = new MapperToNumber(_grammarRules);
        }

        public void Init()
        {
            _mapperToNumber.Initialize();
            table = new List<ISymbol>[_mapperToNumber.VariableCount, _mapperToNumber.TerminalCount];
        }

        public List<ISymbol>[,] ProcessTable()
        {
            Preprocessor preprocessor = new Preprocessor(_grammarRules);
            foreach (ISymbol symbolsValue in _grammarRules.SymbolList)
            {
                if (symbolsValue is Variable variable)
                {
                    var variableNumber = _mapperToNumber.MapVariableToNumber[variable.Value];
                    foreach (IEnumerable<ISymbol> variableDefinition in variable.RuleSet.Definitions)
                    {
                        var firsts=preprocessor.FirstSet(new List<IEnumerable<ISymbol>>() {variableDefinition});
                        
                        foreach (Terminal terminal in firsts.Where(term=>!term.Equals(Terminal.Epsilon)))
                        {
                            table[variableNumber, _mapperToNumber.MapTerminalToNumber[terminal.Value]] = variableDefinition.ToList();
                        }

                        if (firsts.Contains(Terminal.Epsilon))
                        {
                            var variableFollows = variable.Follows;
                            foreach (Terminal variableFollow in variableFollows)
                            {
                                var tableItem = table[variableNumber, _mapperToNumber.MapTerminalToNumber[variableFollow.Value]];
                                if(tableItem==null) 
                                    table[variableNumber, _mapperToNumber.MapTerminalToNumber[variableFollow.Value]] = new List<ISymbol> {Terminal.Epsilon, }.ToList();
                                else // error
                                {
                                    tableItem.Add(Terminal.Error);
                                    tableItem.Add(variableFollow);
                                }
                            }
                        }
                    }
                }
            }

            return table;
        }

        public bool Parse(List<Terminal> terminals)
        {
            terminals.Add(Terminal.EndOfFile);
            var terminalArray = terminals.ToArray();
            Stack<ISymbol> stack = new Stack<ISymbol>();
            stack.Push(Terminal.EndOfFile);
            stack.Push(_grammarRules.HeadVariable);

            _progress.Report(new ParseReportModel()
            {
                InputString = string.Join<ISymbol>("",terminalArray),
                Output = "Poping the stack",
                Stack = string.Join("",stack.ToArray().Reverse()),
            });

            int position = 0;
            while (stack.Count > 0 && position<terminalArray.Length)
            {
                ParseReportModel parseReport = new ParseReportModel();
                parseReport.InputString = string.Join("", terminalArray.Skip(position));
                var current = terminalArray[position];
                var popedValue=stack.Pop();
                if (popedValue is Terminal terminal)
                {
                    if (terminal.Equals(current))
                    {
                        if (current.Equals(Terminal.EndOfFile)) return true;
                        position++;
                        parseReport.Output = $"Matched {current} Terminal";
                    }
                }
                else if (popedValue is Variable variable)
                {
                    var itemsToBepushed=table[_mapperToNumber.MapVariableToNumber[variable.Value], _mapperToNumber.MapTerminalToNumber[current.Value]];
                    if (itemsToBepushed != null)
                    {
                        Stack<ISymbol> reversed = new Stack<ISymbol>();
                        foreach (ISymbol symbol in itemsToBepushed)
                        {
                            reversed.Push(symbol);
                        }

                        parseReport.Output ="Pushing "+ string.Join("",itemsToBepushed);
                        while(reversed.Count>0)
                            if (reversed.Peek().Equals(Terminal.Epsilon)) break;
                                else stack.Push(reversed.Pop());
                    }
                }

                parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                _progress.Report(parseReport);
            }
            
            return false;
        }
        
    }
}

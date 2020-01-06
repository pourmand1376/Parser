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
        public MapperToNumber MapperToNumber { get; }

        private Stack<TreeNode> treeNodes;
        private int _orderId = 1;
        public TreeNode BaseNode { get; set; }
        
        public LeftToRight_LookAhead_One(GrammarRules grammarRules,IProgress<ParseReportModel> progress)
        {
            treeNodes = new Stack<TreeNode>();
            BaseNode = new TreeNode(grammarRules.HeadVariable,_orderId);
            treeNodes.Push(BaseNode);
            _grammarRules = grammarRules;
            _progress = progress;
            MapperToNumber = new MapperToNumber(_grammarRules);
        }

        public void Init()
        {
            MapperToNumber.Initialize();
            table = new List<ISymbol>[MapperToNumber.VariableCount, MapperToNumber.TerminalCount];
        }

        public List<ISymbol>[,] ProcessTable()
        {
            Preprocessor preprocessor = new Preprocessor(_grammarRules);
            foreach (ISymbol symbolsValue in _grammarRules.SymbolList)
            {
                if (symbolsValue is Variable variable)
                {
                    var variableNumber = MapperToNumber.MapVariableToNumber[variable.Value];
                    foreach (IEnumerable<ISymbol> variableDefinition in variable.RuleSet.Definitions)
                    {
                        var firsts=preprocessor.FirstSet(new List<IEnumerable<ISymbol>>() {variableDefinition});
                        
                        foreach (Terminal terminal in firsts.Where(term=>!term.Equals(Terminal.Epsilon)))
                        {
                            table[variableNumber, MapperToNumber.MapTerminalToNumber[terminal.Value]] = variableDefinition.ToList();
                        }

                        if (firsts.Contains(Terminal.Epsilon))
                        {
                            var variableFollows = variable.Follows;
                            foreach (Terminal variableFollow in variableFollows)
                            {
                                var tableItem = table[variableNumber, MapperToNumber.MapTerminalToNumber[variableFollow.Value]];
                                if(tableItem==null) 
                                    table[variableNumber, MapperToNumber.MapTerminalToNumber[variableFollow.Value]] = new List<ISymbol> {Terminal.Epsilon, }.ToList();
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

                        var treeNode = treeNodes.Pop();
                    }

                    
                }
                else if (popedValue is Variable variable)
                {
                    var itemsToBepushed=table[MapperToNumber.MapVariableToNumber[variable.Value], MapperToNumber.MapTerminalToNumber[current.Value]];
                    if (itemsToBepushed != null)
                    {
                        Stack<ISymbol> reversed = new Stack<ISymbol>();
                        foreach (ISymbol symbol in itemsToBepushed)
                        {
                            reversed.Push(symbol);
                        }
                        
                        parseReport.Output ="Pushing "+ string.Join("",itemsToBepushed);
                        var father = treeNodes.Pop();
                        while(reversed.Count>0)
                        {
                            if (reversed.Peek().Equals(Terminal.Epsilon))
                            {
                                var pop = reversed.Pop();    
                                father.Nodes.Add(new TreeNode(pop,_orderId++));
                                break;
                            }
                            var symbol = reversed.Pop();
                            stack.Push(symbol);

                            var treeNode = new TreeNode(symbol,_orderId++);
                            treeNodes.Push(treeNode);
                            father.Nodes.Add(treeNode);
                        }
                    }
                }
                else
                {
                    parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                    parseReport.Output = "I Could parse this string";
                    _progress.Report(parseReport);
                    return false;
                }

                parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                _progress.Report(parseReport);
            }
            
            return false;
        }
        
    }
}

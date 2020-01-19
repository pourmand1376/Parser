using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Parser.Lexical;
using Parser.LLTable;
using Parser.Models;
using Parser.State;
using Parser.States;
using Action = Parser.State.Action;

namespace Parser.Parse
{
    public class LeftToRight_RightMost_Zero
    {
        private readonly GrammarRules _grammarRules;
        private readonly LRType _lrType;
        private readonly Preprocessor _preprocessor;
        private LRGrammarTable _grammarTable;
        public FiniteStateMachine FiniteStateMachine { get; }

        public Stack<TreeNode> Nodes;
        public int OrderId = 1;
        public  MapperToNumber MapperToNumber { get;  }

        public LeftToRight_RightMost_Zero(GrammarRules grammarRules,LRType lrType,Preprocessor preprocessor)
        {
            _grammarRules = grammarRules;
            _lrType = lrType;
            _preprocessor = preprocessor;
            MapperToNumber = new MapperToNumber(_grammarRules);
            FiniteStateMachine = new FiniteStateMachine(_grammarRules,_preprocessor,lrType==LRType.ClR_One);
            Nodes = new Stack<TreeNode>();
        }

        public string CalculateStateMachine()
        {
            MapperToNumber.Initialize();
            FiniteStateMachine.InitializeAllStates();
            return FiniteStateMachine.ToString();
        }
        
        public LRGrammarTable FillTable()
        {
            _grammarTable = new LRGrammarTable(FiniteStateMachine,MapperToNumber,_lrType);
            _grammarTable.Init(); 
            _grammarTable.FillTable(_grammarRules.HeadVariable);
            return _grammarTable;
        }

        public bool Parse(List<Terminal> terminals,IProgress<ParseReportModel> report)
        {
            Terminal[] text = terminals.ToArray();
            Stack stack = new Stack();
            stack.Push(0);
            ParseReportModel firstReport = new ParseReportModel();
            firstReport.InputString = string.Join<Terminal>("", text);
            firstReport.Output = "Pushing 0 to the stack";
            firstReport.Stack = string.Join("", stack.ToArray());
            report.Report(firstReport);
            int position = 0;
            while (stack.Count > 0)
            {
                ParseReportModel parseReport = new ParseReportModel();
                parseReport.Stack =string.Join("",stack.ToArray().Reverse());
                parseReport.InputString = string.Join("", terminals.Skip(position));
               
                int peek = (int) stack.Peek();
                var textPosition = text[position];
                var parserAction = _grammarTable.GetParserAction(peek, textPosition);
                if (parserAction == null)
                {
                    parseReport.Output = "No Parser Action is found";
                    report.Report(parseReport);
                    return false;
                }

                if (parserAction.Action == Action.Accept)
                {
                    parseReport.Output = "Success";
                    report.Report(parseReport);
                    return true;
                }
                if (parserAction.Action == Action.Reduce)
                {
                    if (!Reduce(terminals, report, parserAction, stack, parseReport, position)) return false;
                }
                else if (parserAction.Action == Action.Shift)
                {
                    position = Shift(position, stack, textPosition, parserAction, parseReport);
                }
                
                parseReport.InputString = string.Join("", terminals.Skip(position));
                parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                report.Report(parseReport);
            }

            return false;
        }

        private int Shift(int position, Stack stack, Terminal textPosition, ParserAction parserAction,
            ParseReportModel parseReport)
        {
            position++;
            stack.Push(textPosition);
            stack.Push(parserAction.ShiftState);
            parseReport.Output = "Shift to state " + parserAction.ShiftState;
            Nodes.Push(new TreeNode(textPosition,OrderId++));
            return position;
        }

        private bool Reduce(List<Terminal> terminals, IProgress<ParseReportModel> report, ParserAction parserAction, Stack stack,
            ParseReportModel parseReport, int position)
        {
            int peek;
            int countpop = parserAction.Handle.Count();
            ISymbol[] symbols = parserAction.Handle.Reverse().ToArray();
            if (!parserAction.Handle.Contains(Terminal.Epsilon))
            {
                TreeNode node = new TreeNode(parserAction.Variable,OrderId++);
                for (int i = 0; i < countpop; i++)
                {
                    stack.Pop(); //pop the number
                    ISymbol pop = (ISymbol) stack.Pop();
                    node.Nodes.Add(Nodes.Pop());
                    if (!symbols[i].Equals(pop))
                    {
                        parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                        parseReport.InputString = string.Join("", terminals.Skip(position));
                        parseReport.Output = "symbol is not equal to the top of stack";
                        report.Report(parseReport);
                        return false;
                    }
                }
                Nodes.Push(node);
            }
            else
            {
                var item = new TreeNode(Terminal.Epsilon,OrderId++);
                var treeNode = new TreeNode(parserAction.Variable,OrderId++);
                treeNode.Nodes.Add(item);
                Nodes.Push(treeNode);
            }

            peek = (int) stack.Peek();
            stack.Push(parserAction.Variable);
            var goTo = _grammarTable.GetGoTo(peek, parserAction.Variable);
            if (goTo == null)
            {
                parseReport.Stack = string.Join("", stack.ToArray().Reverse());
                parseReport.Output = "GoTo table is null";
                report.Report(parseReport);
                return false;
            }

            stack.Push(goTo.StateId);
            parseReport.Output =
                $"Reduce {countpop}.Push {parserAction.Variable}::={string.Join("", parserAction.Handle)} .GoTo {goTo.StateId}";
            return true;
        }
    }
}

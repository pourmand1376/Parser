using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Parser.Lexical;
using Parser.Models;
using Parser.State;
using Parser.States;
using Action = Parser.State.Action;

namespace Parser.Parse
{
    public class LeftToRight_RightMost_Zero
    {
        private readonly GrammarRules _grammarRules;
        private LRGrammarTable _grammarTable;
        public FiniteStateMachine FiniteStateMachine { get; }

        public  MapperToNumber MapperToNumber { get;  }

        public LeftToRight_RightMost_Zero(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            MapperToNumber = new MapperToNumber(_grammarRules);
            FiniteStateMachine = new FiniteStateMachine(_grammarRules);
        }

        public string CalculateStateMachine()
        {
            MapperToNumber.Initialize();
            FiniteStateMachine.InitializeAllStates();
            return FiniteStateMachine.ToString();
        }
        
        public LRGrammarTable FillTable()
        {
            _grammarTable = new LRGrammarTable(FiniteStateMachine,MapperToNumber);
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

                int peek = (int) stack.Peek();
                var textPosition = text[position];
                var parserAction = _grammarTable.GetParserAction(peek, textPosition);
                if (parserAction == null) return false;

                if (parserAction.Action == Action.Accept)
                {
                    parseReport.Stack =string.Join("",stack.ToArray());
                    parseReport.Output = "Success";
                    report.Report(parseReport);
                    return true;
                }
                if (parserAction.Action == Action.Reduce)
                {
                    int countpop = parserAction.Handle.Count();
                    ISymbol[] symbols = parserAction.Handle.Reverse().ToArray();
                    for (int i = 0; i < countpop; i++)
                    {
                        stack.Pop();//pop the number
                        ISymbol pop = (ISymbol) stack.Pop();
                        if (!symbols[i].Equals(pop))
                        {
                            return false;
                        }
                    }

                    peek = (int) stack.Peek();    
                    stack.Push(parserAction.Variable);
                    var goTo = _grammarTable.GetGoTo(peek, parserAction.Variable);
                    if (goTo == null) return false;
                    stack.Push(goTo.StateId);
                    parseReport.Output = $"Reduce {countpop}.Push {parserAction.Variable} .GoTo {goTo}";
                }
                else if (parserAction.Action == Action.Shift)
                {
                    position++;
                    stack.Push(textPosition);
                    stack.Push(parserAction.ShiftState);
                    parseReport.Output = "Shift to state " + parserAction.ShiftState;
                }
                
                parseReport.InputString = string.Join("", terminals.Skip(position));
                parseReport.Stack = string.Join("", stack.ToArray());
                report.Report(parseReport);
            }

            return false;
        }
    }
}

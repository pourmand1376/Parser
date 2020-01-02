using System.Collections.Generic;
using System.Linq;
using Parser.Models;
using Parser.Parse;
using Parser.States;

namespace Parser.State
{
    /// <summary>
    /// Containing All LR Table Information
    /// GoTo
    /// Action
    /// </summary>
    public class LLGrammarTable
    {
        private readonly FiniteStateMachine _fsm;
        private readonly MapperToNumber _mapperToNumber;
        public ParserAction[,] ActionTable { get; set; }
        public int[,] GoToTable { get; set; }

        public LLGrammarTable(FiniteStateMachine fsm,MapperToNumber mapperToNumber)
        {
            _fsm = fsm;
            _mapperToNumber = mapperToNumber;
        }

        public void Init()
        {
            ActionTable = new ParserAction[_fsm.States.Count, _mapperToNumber.TerminalCount];
            GoToTable = new int[_fsm.States.Count, _mapperToNumber.VariableCount];
        }

        public void FillTable()
        {
            foreach (States.State currentState in _fsm.States)
            {
                //reduce
                if (!currentState.NextStates.Any())
                {
                    ParserAction parser = new ParserAction();
                    parser.Action = Action.Reduce;
                    foreach (var rowState in currentState.RowStates)
                    {
                        parser.Variable = rowState.Variable;
                        parser.Handle = rowState.Rule;
                    }
                    
                    for (int i = 0; i < _mapperToNumber.TerminalCount; i++)
                    {

                    }
                    continue;
                }
                foreach (KeyValuePair<ISymbol, States.State> fsmStateNextState in currentState.NextStates)
                {
                    //shift
                    if (fsmStateNextState.Key is Terminal terminal)
                    {
                        ParserAction action = new ParserAction
                        {
                            ShiftState = fsmStateNextState.Value.StateId, Action = Action.Shift
                        };
                        ActionTable[currentState.StateId, _mapperToNumber.Map(terminal)] =
                            action;
                    }
                    else if (fsmStateNextState.Key is Variable variable)
                    {
                        GoToTable[currentState.StateId, _mapperToNumber.Map(variable)] =
                            fsmStateNextState.Value.StateId;
                    }
                }
            }
        }
    }
}

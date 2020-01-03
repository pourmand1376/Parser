using Parser.Models;
using Parser.Parse;
using Parser.States;
using System.Collections.Generic;
using System.Linq;
using Parser.LLTable;

namespace Parser.State
{
    /// <summary>
    /// Containing All LR Table Information
    /// GoTo
    /// Action
    /// </summary>
    public class LRGrammarTable
    {
        private readonly FiniteStateMachine _fsm;
        private readonly MapperToNumber _mapperToNumber;
        private readonly LRType _lrType;
        public ParserAction[,] ActionTable { get; set; }
        public GoTo[,] GoToTable { get; set; }

        public LRGrammarTable(FiniteStateMachine fsm, MapperToNumber mapperToNumber, LRType lrType)
        {
            _fsm = fsm;
            _mapperToNumber = mapperToNumber;
            _lrType = lrType;
        }

        public void Init()
        {
            ActionTable = new ParserAction[_fsm.States.Count, _mapperToNumber.TerminalCount];
            GoToTable = new GoTo[_fsm.States.Count, _mapperToNumber.VariableCount];
        }

        public ParserAction GetParserAction(int state, Terminal terminal)
        {
            return ActionTable[state, _mapperToNumber.Map(terminal)];
        }

        public GoTo GetGoTo(int state,Variable variable)
        {
            return GoToTable[state, _mapperToNumber.Map(variable)];
        }

        public void AddParseActionToTable(int row,int cell,ParserAction parserAction)
        {
            if (ActionTable[row, cell] == null)
                ActionTable[row, cell] = parserAction;
            else
            {
                if(!ActionTable[row,cell].Equals(parserAction))
                    ActionTable[row, cell].ErrorAction = parserAction;
            }
        }
        public void FillTable(Variable head)
        {
            
            foreach (States.State currentState in _fsm.States)
            {
                AddState(head, currentState);
            }
        }

        private void AddState(Variable head, States.State currentState)
        {
            AddReduceAccept(head, currentState);
            AddShiftGo(currentState);
        }

        private void AddShiftGo(States.State currentState)
        {
            foreach (KeyValuePair<ISymbol, States.State> fsmStateNextState in currentState.NextStates)
            {
                //shift
                if (fsmStateNextState.Key is Terminal terminal)
                {
                    ParserAction action = new ParserAction
                    {
                        ShiftState = fsmStateNextState.Value.StateId,
                        Action = Action.Shift
                    };
                    AddParseActionToTable(currentState.StateId, _mapperToNumber.Map(terminal), action);
                }
                //goto
                else if (fsmStateNextState.Key is Variable variable)
                {
                    GoToTable[currentState.StateId, _mapperToNumber.Map(variable)] =
                        new GoTo(fsmStateNextState.Value.StateId);
                }
            }
        }

        private void AddReduceAccept(Variable head, States.State currentState)
        {
            foreach (RowState currentStateRowState in currentState.RowStates)
            {
                ParserAction parser = new ParserAction();
                //if some rule is finished it means reduce or accept
                if (!currentStateRowState.Finished) continue;

                if (currentStateRowState.Variable.Equals(head))
                {
                    parser.Action = Action.Accept;
                    AddParseActionToTable(currentState.StateId, _mapperToNumber.Map(Terminal.EndOfFile), parser);
                }
                else
                {
                    parser.Action = Action.Reduce;
                    parser.Variable = currentStateRowState.Variable;
                    parser.Handle = currentStateRowState.Rule;

                    if (_lrType == LRType.Zero)
                    {
                        for (int i = 0; i < _mapperToNumber.TerminalCount; i++)
                        {
                            AddParseActionToTable(currentState.StateId, i, parser);
                        }
                    }
                    else if (_lrType == LRType.SLR_One)
                    {
                        foreach (Terminal terminal in currentStateRowState.Variable.Follows)
                        {
                            AddParseActionToTable(currentState.StateId, _mapperToNumber.Map(terminal), parser);
                        }
                    }
                }
            }
        }
    }
}

using Parser.Models;
using Parser.Parse;
using Parser.States;
using System.Collections.Generic;
using System.Linq;

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
        public ParserAction[,] ActionTable { get; set; }
        public GoTo[,] GoToTable { get; set; }

        public LRGrammarTable(FiniteStateMachine fsm, MapperToNumber mapperToNumber)
        {
            _fsm = fsm;
            _mapperToNumber = mapperToNumber;
        }

        public void Init()
        {
            ActionTable = new ParserAction[_fsm.States.Count, _mapperToNumber.TerminalCount];
            GoToTable = new GoTo[_fsm.States.Count, _mapperToNumber.VariableCount];
        }

        public void AddParseActionToTable(int row,int cell,ParserAction parserAction)
        {
            if (ActionTable[row, cell] == null)
                ActionTable[row, cell] = parserAction;
            else
            {
                var action = ActionTable [row,cell];
                if(!action.Equals(parserAction))
                    action.ErrorAction = parserAction;
            }
        }
        public void FillTable(Variable head)
        {
            ParserAction parser = new ParserAction();
            foreach (States.State currentState in _fsm.States)
            {
                foreach (RowState currentStateRowState in currentState.RowStates)
                {
                    //if some rule is finished it means reduce or accept
                    if (currentStateRowState.Finished)
                    {
                        if (currentStateRowState.Variable.Equals(head))
                        {
                            parser.Action = Action.Accept;
                            AddParseActionToTable(currentState.StateId,_mapperToNumber.Map(Terminal.EndOfFile),parser);
                        }
                        else
                        {
                            parser.Action = Action.Reduce;
                            parser.Variable = currentStateRowState.Variable;
                            parser.Handle = currentStateRowState.Rule;
                            for (int i = 0; i < _mapperToNumber.TerminalCount; i++)
                            {
                                AddParseActionToTable(currentState.StateId, i, parser);
                            }
                        }
                    }
                }
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
                        AddParseActionToTable(currentState.StateId, _mapperToNumber.Map(terminal),action);
                    }
                    //goto
                    else if (fsmStateNextState.Key is Variable variable)
                    {
                        GoToTable[currentState.StateId, _mapperToNumber.Map(variable)] =
                            new GoTo(fsmStateNextState.Value.StateId);
                    }
                }
            }
        }
    }
}

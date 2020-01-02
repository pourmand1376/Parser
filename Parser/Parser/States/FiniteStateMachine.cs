using Parser.Lexical;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Parser.Models;

namespace Parser.States
{
    public class FiniteStateMachine
    {
        private readonly GrammarRules _grammarRules;
        public HashSet<State> States { get; }

        public FiniteStateMachine(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            States = new HashSet<State>();
        }
        public void InitializeAllStates()
        {
            Queue<State> queue = new Queue<State>();

            State firstState = new State();
            foreach (var rule in _grammarRules.HeadVariable.RuleSet.Definitions)
            {
                firstState.AddRowState(new RowState(_grammarRules.HeadVariable,rule));
            }
            firstState.AddClosures();
            queue.Enqueue(firstState);
            int stateNo = 0;
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (States.Contains(state))
                {
                    continue;
                }

                state.StateId = stateNo++;
                States.Add(state);
                //if it's not the first State
                if (state.PreviousState != null)
                {
                    if (!state.PreviousState.NextStates.ContainsKey(state.TransferredSymbol))
                    {
                        state.PreviousState.NextStates.Add(state.TransferredSymbol,state);
                    }
                }

                //producing new items
                var extractFirstSymbol = state.ExtractFirstSymbol().Distinct();
                foreach (ISymbol symbol in extractFirstSymbol)
                {
                    var nextState = state.CreateNextState(symbol);
                    if (nextState.RowStates.Count > 0)
                    {
                        nextState.PreviousState = state;
                        nextState.TransferredSymbol = symbol;
                        nextState.AddClosures();
                        if (nextState.Equals(state))
                        {
                            state.NextStates.Add(symbol,state);
                        }
                        if(!States.Contains(nextState))
                            queue.Enqueue(nextState);
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Join("\n-------------\n", States);
        }
    }

}

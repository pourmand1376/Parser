using Parser.Lexical;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;

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
                firstState.AddRowState(new RowState(_grammarRules.HeadVariable, rule));
            }
            firstState.AddClosures();
            queue.Enqueue(firstState);
            States.Add(firstState);
            int stateNo = 1;
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                
                //producing new items
                var extractFirstSymbol = state.ExtractFirstSymbol().Distinct();
                foreach (ISymbol symbol in extractFirstSymbol)
                {
                    var nextState = state.CreateNextState(symbol);
                    nextState.AddClosures();
                    if (nextState.RowStates.Count > 0)
                    {
                        nextState.PreviousState = state;
                        nextState.TransferredSymbol = symbol;

                        //It's right not to add the state
                        if (!States.Contains(nextState))
                        {
                            queue.Enqueue(nextState);
                            nextState.StateId = stateNo;
                            stateNo++;
                            States.Add(nextState);

                            //if it's not the first State
                            if (!state.NextStates.ContainsKey(symbol))
                            {
                                state.NextStates.Add(symbol, nextState);
                            }

                        }
                        else
                        {//but we should add next state to know where to go
                            state.NextStates.Add(symbol, States.First(f => f.Equals(nextState)));
                        }
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

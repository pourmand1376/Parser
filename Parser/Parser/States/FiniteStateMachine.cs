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
            queue.Enqueue(firstState);
            
            while (queue.Count > 0)
            {
                var state = queue.Dequeue();
                if (States.Contains(state))
                {
                    continue;
                }
                States.Add(state);
                state.AddClosures();
                var extractFirstSymbol = state.ExtractFirstSymbol().Distinct();
                foreach (ISymbol symbol in extractFirstSymbol)
                {
                    var nextState = state.CreateNextState(symbol);
                    if (nextState.RowStates.Count > 0)
                    {
                        nextState.PreviousState = state;
                        nextState.TransferredSymbol = symbol;
                        if(!queue.Contains(nextState))
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

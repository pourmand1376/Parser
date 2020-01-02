using Parser.Lexical;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            Stack<State> stack = new Stack<State>();

            State firstState = new State();
            foreach (var rule in _grammarRules.HeadVariable.RuleSet.Definitions)
            {
                firstState.AddRowState(new RowState(_grammarRules.HeadVariable,rule));
            }
            stack.Push(firstState);
            while (stack.Count > 0)
            {
                var state = stack.Pop();
                if (!States.Contains(state))
                {
                    continue;
                }
                States.Add(state);
                state.AddClosures();
                var extractFirstSymbol = state.ExtractFirstSymbol();
                foreach (ISymbol symbol in extractFirstSymbol)
                {
                    var nextState = state.CreateNextState(symbol);
                    stack.Push(nextState);
                }
            }
        }
    }
}

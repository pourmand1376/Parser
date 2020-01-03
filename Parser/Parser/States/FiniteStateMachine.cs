using Parser.Lexical;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;
using Parser.Parse;

namespace Parser.States
{
    public class FiniteStateMachine
    {
        private readonly GrammarRules _grammarRules;
        private readonly bool _isClr;
        public HashSet<State> States { get; }
        private Preprocessor _preprocessor;
        public FiniteStateMachine(GrammarRules grammarRules,Preprocessor preprocessor, bool isClr)
        {
            _grammarRules = grammarRules;
            _isClr = isClr;
            States = new HashSet<State>();
            _preprocessor = preprocessor;
        }
        public void InitializeAllStates()
        {
            Queue<State> queue = new Queue<State>();

            State firstState = new State(_preprocessor,_isClr);
            foreach (var rule in _grammarRules.HeadVariable.RuleSet.Definitions)
            {
                var rowState = new RowState(_grammarRules.HeadVariable, rule);
                if(_isClr) rowState.LookAhead = new List<Terminal>(){Terminal.EndOfFile};
                firstState.AddRowState(rowState);
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

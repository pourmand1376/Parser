using Parser.Models;
using System.Collections.Generic;
using System.Linq;

namespace Parser.States
{
    /// <summary>
    /// represeting a state
    /// A ::= .B
    /// A ::= .C
    /// B ::= .B
    /// </summary>
    public class State
    {
        public int StateId { get; set; }
        public HashSet<RowState> RowStates { get; }

        public State PreviousState { get; set; }
        public ISymbol TransferredSymbol { get; set; }

        public Dictionary<ISymbol, State> NextStates { get; set; }
        public State()
        {
            RowStates = new HashSet<RowState>();
            NextStates = new Dictionary<ISymbol, State>();
        }

        public void AddRowState(RowState row)
        {
            if (!RowStates.Contains(row))
                RowStates.Add(row);
        }

        /// <summary>
        /// the order is horrible. I did this to support Recursive LL(1) Grammers
        /// Error :Collection was modified.
        /// </summary>
        public void AddClosures()
        {

            bool changed;
            do
            {
                changed = false;
                foreach (RowState state in RowStates.ToList())
                {
                    if (state.GetSymbolInPosition() is Variable variable)
                    {
                        variable.RuleSet.Definitions
                            .ForEach(rule =>
                            {
                                RowState rowState = new RowState(variable, rule);
                                if (!RowStates.Contains(rowState))
                                {
                                    RowStates.Add(rowState);
                                    changed = true;
                                }
                            });
                    }
                }
            } while (changed);
        }

        public IEnumerable<ISymbol> ExtractFirstSymbol()
        {
            foreach (RowState rowState in RowStates)
            {
                var symbol = rowState.GetSymbolInPosition();
                if (symbol != null && !symbol.Equals(Terminal.Epsilon)) yield return symbol;
            }
        }

        public State CreateNextState(ISymbol symbol)
        {
            State newstate = new State();
            foreach (RowState rowState in RowStates)
            {
                var symbolInPosition = rowState.GetSymbolInPosition();
                if (symbolInPosition != null && symbolInPosition.Equals(symbol))
                {
                    RowState newRowState = rowState.Clone();
                    newRowState.IncrementPosition();
                    newstate.AddRowState(newRowState);
                }
            }
            return newstate;
        }

        public override bool Equals(object obj)
        {
            if (obj is State state)
            {
                var result = RowStates.SetEquals(state.RowStates);
                return result;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 1;
            foreach (RowState rowState in RowStates)
            {
                hash = (hash * 17) + rowState.GetHashCode();
            }

            return hash;
        }

        public override string ToString()
        {
            var nextState = string.Join(", ", from next in NextStates
                                              select next.Key + ":" + next.Value.StateId);
            return $"{StateId}\n " +
                   $"Comming From {PreviousState?.StateId.ToString() ?? "God"} with {TransferredSymbol}\n" +
                   string.Join("\n", RowStates) + "\n\n" + nextState;
        }
    }
}

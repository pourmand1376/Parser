using Parser.Models;
using System.Collections.Generic;
using System.Linq;
using Parser.Parse;

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
        private readonly Preprocessor _preprocessor;
        private readonly bool _isClr;
        public int StateId { get; set; }
        /// <summary>
        /// only identifier of any state
        /// </summary>
        public HashSet<RowState> RowStates { get; }

        public State PreviousState { get; set; }
        public ISymbol TransferredSymbol { get; set; }

        public Dictionary<ISymbol, State> NextStates { get; set; }

        public bool ReduceOnly => RowStates.All(f => f.Finished);
        public bool ShiftOnly => RowStates.All(f => !f.Finished);
        public bool PossibleConflict => !ReduceOnly && !ShiftOnly;

        public State(Preprocessor preprocessor,bool isClr = false)
        {
            _preprocessor = preprocessor;
            _isClr = isClr;
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
                        var defaultLookAhead = state.LookAhead;
                        // if there is no B, 
                        if (_isClr && state.HasSymbolAfterPosition())
                        {
                            //first (B{PreviousLookAhead})
                            var symbolsAfterPosition = state.GetSymbolsAfterPosition().ToList();
                            //symbolsAfterPosition.AddRange(defaultLookAhead);
                            defaultLookAhead=_preprocessor.FirstSet(new List<IEnumerable<ISymbol>> {symbolsAfterPosition});
                            if (defaultLookAhead.Contains(Terminal.Epsilon))
                            {
                                defaultLookAhead.Remove(Terminal.Epsilon);
                                defaultLookAhead.AddRange(state.LookAhead);
                            }
                        }
                        variable.RuleSet.Definitions
                            .ForEach(rule =>
                            {
                                RowState rowState = new RowState(variable, rule);
                                if(_isClr) rowState = new RowState(variable,rule,defaultLookAhead);
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
            State newstate = new State(_preprocessor,_isClr);
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

        public string ToStringCompact()
        {
            var shiftReduce = ShiftOnly ? "ShiftOnly" : ReduceOnly ? "ReduceOnly" : "Possible Conflict.";
            return $"State No:{StateId}\n{string.Join("\n", RowStates)}\n\n{shiftReduce}";
        }

    }
}

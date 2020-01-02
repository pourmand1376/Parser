using System.Collections;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

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

        public HashSet<RowState> RowStates { get; }

        public State PreviousState { get; set; }
        public ISymbol TransferredSymbol { get; set; }

        public State()
        {
            RowStates = new HashSet<RowState>();
            
        }

        public void AddRowState(RowState row)
        {
            if (!RowStates.Contains(row))
                RowStates.Add(row);
        }

        public void AddClosures()
        {
            ArrayList hashStates = new ArrayList(RowStates.ToList());
            for (int i = 0; i < hashStates.Count; i++)
            {
                if (((RowState)hashStates[i]).GetSymbolInPosition() is Variable variable)
                {
                    variable.RuleSet.Definitions
                        .ForEach(rule => hashStates.Add(new RowState(variable, rule)));
                }
            }
            foreach (RowState state in hashStates)
            {
                if (!RowStates.Contains(state))
                    RowStates.Add(state);
            }
        }

        public IEnumerable<ISymbol> ExtractFirstSymbol()
        {
            foreach (RowState rowState in RowStates)
            {
                var symbol= rowState.GetSymbolInPosition();
                if (symbol != null && !symbol.Equals(Terminal.Epsilon)) yield return symbol;
            }
        }

        public State CreateNextState(ISymbol symbol)
        {
            State newstate = new State();
            foreach (RowState rowState in RowStates.Where(f=>!f.Finished))
            {
                var symbolInPosition = rowState.GetSymbolInPosition();
                if (symbolInPosition!=null && symbolInPosition.Equals(symbol))
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
                var result= RowStates.SetEquals(state.RowStates);
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
            return string.Join("\n", RowStates);
        }
    }
}

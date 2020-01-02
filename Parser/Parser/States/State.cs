using Parser.Models;
using System.Collections.Generic;

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

        public Dictionary<ISymbol, State> SymbolState { get; }

        public State()
        {
            RowStates = new HashSet<RowState>();
            SymbolState = new Dictionary<ISymbol, State>();
        }

        public void AddRowState(RowState row)
        {
            if (!RowStates.Contains(row))
                RowStates.Add(row);
        }

        public void AddClosures()
        {
            foreach (RowState rowState in RowStates)
            {
                if (rowState.GetSymbolInPosition() is Variable variable)
                {
                    variable.RuleSet.Definitions
                        .ForEach(rule => RowStates.Add(new RowState(variable, rule)));
                }
            }
        }

        public IEnumerable<ISymbol> ExtractFirstSymbol()
        {
            foreach (RowState rowState in RowStates)
            {
                yield return rowState.GetSymbolInPosition();
            }
        }

        public State CreateNextState(ISymbol symbol)
        {
            State newstate= new State();
            foreach (RowState rowState in RowStates)
            {
                if (rowState.GetSymbolInPosition().Equals(symbol))
                {
                    RowState newRowState=rowState.Clone();
                    newRowState.IncrementPosition();
                }
            }
            return newstate;
        }

        public void AddStateToDictionary(ISymbol symbol, State state)
        {
            if(!SymbolState.ContainsKey(symbol))
                SymbolState.Add(symbol,state);
        }
        public override bool Equals(object obj)
        {
            if (obj is State state)
            {
                return RowStates.Equals(state.RowStates);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return RowStates.GetHashCode();
        }
    }
}

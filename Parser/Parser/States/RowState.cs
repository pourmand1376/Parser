using System;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;

namespace Parser.States
{
    /// <summary>
    /// represeting just a single row of a state
    /// A ::= .B
    /// </summary>
    public class RowState
    {
        /// <summary>
        /// . 
        /// </summary>
        public int Position { get;private set; }

        public Variable Variable { get; }

        public IEnumerable<ISymbol> Rule { get; }

        public RowState(Variable variable, IEnumerable<ISymbol> rule, int position = 0)
        {
            Variable = variable;
            Rule = rule;
            Position = position;
        }

        public ISymbol GetSymbolInPosition()
        {
            int position = 0;
            foreach (ISymbol symbol in Rule)
            {
                if (position == Position)
                {
                    return symbol;
                }
                position++;
            }
            throw new Exception("Position is not valid");
        }

        public void IncrementPosition()
        {
            Position++;
        }

        public RowState Clone()
        {
            return new RowState(Variable,Rule,Position);
        }

        public override bool Equals(object obj)
        {
            if (obj is RowState rowState)
            {
                if (rowState.Variable == this.Variable && rowState.Position == this.Position)
                {
                    return Rule.ToList().Equals(rowState.Rule);
                }
            }
            return false;
        }

    }
}
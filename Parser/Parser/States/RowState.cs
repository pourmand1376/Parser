using System;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool Finished => Position >= Rule.Count();

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
            return null;
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
                if (rowState.Variable.Equals(Variable) && rowState.Position == this.Position)
                {
                    return Rule.ToList().SequenceEqual(rowState.Rule);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Position.GetHashCode();
            hash = (hash * 7) + Variable.GetHashCode();
            foreach (ISymbol symbol in Rule)
            {
                hash = (hash * 17) + symbol.GetHashCode();
            } 
            return hash;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int position = 0;
            bool printed = false;
            foreach (ISymbol symbol in Rule)
            {
                if (position == Position)
                {
                    stringBuilder.Append(" ☺ ");
                    printed = true;
                }
                stringBuilder.Append(symbol);
                position++;
            }
            if(!printed) stringBuilder.Append(" ☺ ");
            return $"{Variable} ::= {stringBuilder}";
        }
    }
}
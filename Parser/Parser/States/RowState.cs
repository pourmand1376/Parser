using System;
using Parser.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        public bool Finished => Position >= Rule.ToList().Count 
                                || Rule.First().Equals(Terminal.Epsilon);

        public List<Terminal> LookAhead { get; set; }

        public RowState(Variable variable, IEnumerable<ISymbol> rule, int position = 0)
        {
            Variable = variable;
            Rule = rule;
            Position = position;
        }
        public RowState(Variable variable, IEnumerable<ISymbol> rule, List<Terminal> lookAhead,int position = 0)
        :this(variable,rule,position)
        {
            LookAhead = lookAhead;
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

        public bool HasSymbolAfterPosition()
        {
            int position = 0;
            foreach (var symbol in Rule)
            {
                if (position == Position+1)
                {
                    return true;
                }
                position++;
            }
            return false;
        }

        public IEnumerable<ISymbol> GetSymbolsAfterPosition()
        {
            return Rule.Skip(Position + 1);
        }

        public void IncrementPosition()
        {
            Position++;
        }

        public RowState Clone()
        {
            return new RowState(Variable,Rule,LookAhead,Position);
        }

        public override bool Equals(object obj)
        {
            if (obj is RowState rowState)
            {
                if (rowState.Variable.Equals(Variable) && rowState.Position == this.Position)
                {
                    return Rule.ToList().SequenceEqual(rowState.Rule) &&
                        (LookAhead?.SequenceEqual(rowState.LookAhead)??true);
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

            if (LookAhead != null)
            {
                foreach (Terminal terminal in LookAhead)
                {
                    hash = (hash * 5) + terminal.GetHashCode();
                }
            }

            return hash;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int position = 0;
            bool printed = false;
            bool hasEpsilon = Rule.Contains(Terminal.Epsilon);
            foreach (ISymbol symbol in Rule)
            {
                if (position == Position && !hasEpsilon)
                {
                    stringBuilder.Append(" ☺ ");
                    printed = true;
                }
                stringBuilder.Append(symbol);
                position++;
            }
            if(!printed && !hasEpsilon) stringBuilder.Append(" ☺ ");

            
            if (LookAhead?.Count > 0)
            {
                stringBuilder.Append($",[{string.Join("", LookAhead)}]");
            }
            return $"{Variable} ::= {stringBuilder}";
        }
    }
}
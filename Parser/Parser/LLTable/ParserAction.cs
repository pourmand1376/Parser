using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parser.Models;
using Parser.States;

namespace Parser.State
{
    /// <summary>
    /// action that the parser should do
    /// </summary>
    public class ParserAction
    {
        /// <summary>
        /// redurce vs shift
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// the shift state
        /// </summary>
        public int ShiftState { get; set; }

        /// <summary>
        /// the handle of the operation 
        /// </summary>
        public IEnumerable<ISymbol> Handle { get; set; }

        /// <summary>
        /// variable to be reduced in the place of handle
        /// </summary>
        public Variable Variable { get; set; }

        /// <summary>
        /// if it has error 
        /// </summary>
        public ParserAction ErrorAction { get; set; }

        public bool HasError => ErrorAction != null;

        public override bool Equals(object obj)
        {
            if (obj is ParserAction parserAction)
            {
                if(Action.Equals(parserAction.Action) && 
                    ShiftState.Equals( parserAction.ShiftState) && 
                    (Variable?.Equals( parserAction.Variable) ?? false))
                {
                    return Handle?.ToList().SequenceEqual(parserAction.Handle) ?? false;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Action == Action.Reduce)
            {
                sb.Append("R");
                sb.Append("-" + Variable.Value);
                sb.Append("-> " + string.Join("", Handle));
            }
            else if(Action==Action.Shift)
            {
                sb.Append("S");
                sb.Append(ShiftState);
            }
            else if (Action == Action.Accept)
            {
                sb.Append("Acc");
            }

            if (HasError)
            {
                sb.Append(" | "+ErrorAction);
            }
            return sb.ToString();
        }

    }
}
using System.Collections.Generic;
using System.Text;
using Parser.Models;

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Action == Action.Reduce)
            {
                sb.Append("R");
                sb.Append("-" + Variable.Value);
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

            return sb.ToString();
        }
    }
}
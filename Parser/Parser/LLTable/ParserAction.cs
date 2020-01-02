using System.Collections.Generic;
using Parser.Models;

namespace Parser.State
{
    /// <summary>
    /// action that the parser should do
    /// </summary>
    public abstract class ParserAction
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
    }
}
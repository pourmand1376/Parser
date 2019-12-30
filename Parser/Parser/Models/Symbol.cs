using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Models
{
    public abstract class Symbol
    {
        public SymbolType SymbolType { get; set; }

        public Terminal GetTerminal()
        {
            if (SymbolType == SymbolType.Terminal)
                return (Terminal) this;
            throw new Exception("specified cast is not valid");
        }

        public Variable GetVariable()
        {
            if(SymbolType==SymbolType.Variable)
            return (Variable) this;
            throw new Exception("Specified cast is not valid");
        }
    }
}

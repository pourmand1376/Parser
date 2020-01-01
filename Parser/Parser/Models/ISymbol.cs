using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Models
{
    public interface ISymbol
    {
        SymbolType SymbolType { get;}
        string Value { get; }
    }
}

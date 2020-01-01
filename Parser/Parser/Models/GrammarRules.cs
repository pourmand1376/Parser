﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Models;

namespace Parser.Lexical
{
    public class GrammarRules
    {
        public Dictionary<string,ISymbol> Symbols { get; set; }

        public Variable HeadVariable { get; set; }

        public GrammarRules()
        {
            Symbols = new Dictionary<string, ISymbol>();
        }

        public ISymbol GetOrCreateSymbol(string value,SymbolType symbolType)
        {
            if (symbolType == SymbolType.Terminal)
            {
                if (value == Terminal.EndOfFile.Value)
                    return Terminal.EndOfFile;
                if (value == "")
                    return Terminal.Epsilon;
                if (!Symbols.ContainsKey(value))
                    Symbols.Add(value, new Terminal(value));
                
                return Symbols[value];
            }
            // else    
            if (!Symbols.ContainsKey(value))
                Symbols.Add(value,new Variable(value));
            return Symbols[value];

        }
    }
}

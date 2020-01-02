﻿using System.Collections;
using System.Collections.Generic;
using Parser.Lexical;
using Parser.State;
using Parser.States;

namespace Parser.Parse
{
    public class LeftToRight_RightMost_Zero
    {
        private readonly GrammarRules _grammarRules;
        public FiniteStateMachine FiniteStateMachine { get; }
        public  MapperToNumber MapperToNumber { get;  }
        public LeftToRight_RightMost_Zero(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            MapperToNumber = new MapperToNumber(_grammarRules);
            FiniteStateMachine = new FiniteStateMachine(_grammarRules);
        }

        public string CalculateStateMachine()
        {
            MapperToNumber.Initialize();
            FiniteStateMachine.InitializeAllStates();
            return FiniteStateMachine.ToString();
        }
        
        public LLGrammarTable FillTable()
        {
            LLGrammarTable grammarTable = new LLGrammarTable(FiniteStateMachine,MapperToNumber);
            grammarTable.Init(); 
            grammarTable.FillTable();
            return grammarTable;
        }
    }
}

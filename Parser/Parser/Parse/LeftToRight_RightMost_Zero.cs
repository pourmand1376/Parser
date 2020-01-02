using System.Collections;
using System.Collections.Generic;
using Parser.Lexical;
using Parser.State;
using Parser.States;

namespace Parser.Parse
{
    public class LeftToRight_RightMost_Zero
    {
        private readonly GrammarRules _grammarRules;
        private FiniteStateMachine _finiteStateMachine;
        private MapperToNumber mapperToNumber;
        public LeftToRight_RightMost_Zero(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
            mapperToNumber = new MapperToNumber(_grammarRules);
        }

        public string CalculateStateMachine()
        {
            _finiteStateMachine = new FiniteStateMachine(_grammarRules);
            _finiteStateMachine.InitializeAllStates();
            return _finiteStateMachine.ToString();
        }
        
        public void FillTable()
        {
            LLGrammarTable grammarTable = new LLGrammarTable(_finiteStateMachine,mapperToNumber);
            grammarTable.Init();
            grammarTable.FillTable();
        }
    }
}

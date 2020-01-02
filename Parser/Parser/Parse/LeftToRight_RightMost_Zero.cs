using System.Collections.Generic;
using Parser.Lexical;
using Parser.States;

namespace Parser.Parse
{
    public class LeftToRight_RightMost_Zero
    {
        private readonly GrammarRules _grammarRules;

        public LeftToRight_RightMost_Zero(GrammarRules grammarRules)
        {
            _grammarRules = grammarRules;
        }

        public string FillStateMachine()
        {
            FiniteStateMachine finiteStateMachine = new FiniteStateMachine(_grammarRules);
            finiteStateMachine.InitializeAllStates();
            return finiteStateMachine.ToString();
        }
        
        public void FillTable()
        {

        }
    }
}

using System.Security.Policy;
using Parser.Lexical;

namespace Parser.State
{
    /// <summary>
    /// Containing All LR Table Information
    /// GoTo
    /// Action
    /// </summary>
    public class LLGrammarTable
    {
        public ParserAction [,] ActionTable;
        public int [,] GoToTable;

        public LLGrammarTable()
        {
            
        }
    }
}

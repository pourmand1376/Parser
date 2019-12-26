using System.CodeDom;

namespace Parser.Models
{
    public class Terminal:Symbol
    {
        public Terminal(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

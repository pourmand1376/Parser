using System.CodeDom;

namespace Parser.Models
{
    public class Terminal:BaseValue
    {
        public Terminal(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}

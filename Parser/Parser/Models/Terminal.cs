using System.CodeDom;

namespace Parser.Models
{
    public class Terminal:Base
    {
        public Terminal(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}

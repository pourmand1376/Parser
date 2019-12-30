namespace Parser.Models
{
    public class Variable:Symbol
    {
        public Variable()
        {
            SymbolType = SymbolType.Variable;
        }
        public Variable(string name):this()
        {
            Name = name;
        }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Variable var)
            {
                return var.Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}

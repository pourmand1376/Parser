namespace Parser.Models
{
    public class Variable:BaseValue
    {
        public Variable()
        {
            
        }
        public Variable(string name)
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
    }
}

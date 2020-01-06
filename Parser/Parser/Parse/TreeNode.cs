using System.Collections.Generic;
using Parser.Models;

namespace Parser.Parse
{
    public class TreeNode
    {
        public ISymbol NodeSymbol { get; }
        public List<TreeNode> Nodes { get; }

        public int OrderId { get; }
        public TreeNode(ISymbol symbol,int orderId)
        {
            NodeSymbol = symbol;
            Nodes = new List<TreeNode>();
            OrderId = orderId;
        }

        public override string ToString()
        {
            return NodeSymbol.ToString()+OrderId; 
        }
    }
}
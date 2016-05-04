namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class IfNode : Node
    {
        public IfNode() : base(NodeType.If)
        {
            
        }

        public string Condition { get; set; }

        public List<Node> IfNodes { get; } = new List<Node>();

        public List<Node> ElseNodes { get; } = new List<Node>();
    }
}

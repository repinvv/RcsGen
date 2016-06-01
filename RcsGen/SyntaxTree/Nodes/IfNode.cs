namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class IfNode : Node
    {
        public IfNode(string condition, List<Node> ifNodes) : base(NodeType.If)
        {
            Condition = condition;
            IfNodes = ifNodes;
            ElseNodes = new List<Node>();
        }

        public IfNode(string condition, List<Node> ifNodes, List<Node> elseNodes) : base(NodeType.If)
        {
            Condition = condition;
            IfNodes = ifNodes;
            ElseNodes = elseNodes;
        }

        public string Condition { get; }

        public List<Node> IfNodes { get; }

        public List<Node> ElseNodes { get; }
    }
}

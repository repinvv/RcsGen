namespace RcsGen.SyntaxTree.Nodes
{
    internal class IfNode : Node
    {
        public IfNode(string condition, NodeStore ifNodes) : base(NodeType.If)
        {
            Condition = condition;
            IfNodes = ifNodes;
            ElseNodes = new NodeStore();
        }

        public IfNode(string condition, NodeStore ifNodes, NodeStore elseNodes) : base(NodeType.If)
        {
            Condition = condition;
            IfNodes = ifNodes;
            ElseNodes = elseNodes;
        }

        public string Condition { get; }

        public NodeStore IfNodes { get; }

        public NodeStore ElseNodes { get; }
    }
}

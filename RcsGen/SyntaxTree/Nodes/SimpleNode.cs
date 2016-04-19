namespace RcsGen.SyntaxTree.Nodes
{
    internal class SimpleNode : INode
    {
        public NodeType NodeType { get; set; }

        public string Content { get; set; }
    }
}

namespace RcsGen.SyntaxTree
{
    internal class SimpleNode : INode
    {
        public NodeType NodeType { get; set; }

        public string Content { get; set; }
    }
}

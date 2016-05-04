namespace RcsGen.SyntaxTree.Nodes
{
    internal class LiteralNode : Node
    {
        public LiteralNode(string content) : base(NodeType.Literal)
        {
            Content = content;
        }

        public string Content { get; }
    }
}

namespace RcsGen.SyntaxTree.Nodes
{
    internal class ContentNode : Node
    {
        public ContentNode(string content, NodeType nodeType) : base(nodeType)
        {
            Content = content;
        }

        public string Content { get; }
    }
}

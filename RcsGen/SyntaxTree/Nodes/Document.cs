namespace RcsGen.SyntaxTree.Nodes
{
    internal class Document : Node
    {
        public Document(NodeStore nodes) : base(NodeType.Document)
        {
            Nodes = nodes;
        }

        public NodeStore Nodes { get; }
    }
}

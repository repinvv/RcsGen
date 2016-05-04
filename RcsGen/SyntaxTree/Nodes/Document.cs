namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class Document : Node
    {
        public Document() : base(NodeType.Document)
        {
        }

        public List<Node> Nodes { get; } = new List<Node>();
    }
}

namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class Document : Node
    {
        public Document(List<Node> nodes) : base(NodeType.Document)
        {
            Nodes = nodes;
        }

        public List<Node> Nodes { get; }
    }
}

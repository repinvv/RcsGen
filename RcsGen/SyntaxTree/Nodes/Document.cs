namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class Document : Node
    {
        public Document(NodeStore nodes) : base(NodeType.Document)
        {
            Nodes = nodes;
        }

        public NodeStore Nodes { get; }
    }
}

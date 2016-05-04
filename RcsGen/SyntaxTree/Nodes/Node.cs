namespace RcsGen.SyntaxTree.Nodes
{
    internal class Node
    {
        public Node(NodeType nodeType)
        {
            NodeType = nodeType;
        }

        public NodeType NodeType { get; }
    }
}

namespace RcsGen.LowLevelTree.Nodes
{
    internal class LiteralNode : INode
    {
        public LiteralNode(string literal)
        {
            Literal = literal;
        }

        public NodeType NodeType => NodeType.Literal;

        public string Literal { get; set; }
    }
}

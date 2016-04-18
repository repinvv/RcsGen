namespace RcsGen.SyntaxTree
{
    internal class ConfigNode : INode
    {
        public NodeType NodeType => NodeType.Config;

        public TYPE Type { get; set; }
    }
}

namespace RcsGen.SyntaxTree.Nodes
{
    internal class ConfigNode : INode
    {
        public NodeType NodeType => NodeType.Config;

        public ConfigCommand ConfigCommand { get; set; }

        public string Parameters { get; set; }
    }
}

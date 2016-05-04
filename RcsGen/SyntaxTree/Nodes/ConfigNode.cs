namespace RcsGen.SyntaxTree.Nodes
{
    internal class ConfigNode : Node
    {
        public ConfigNode(ConfigCommand configCommand, string parameters) : base(NodeType.Config)
        {
            ConfigCommand = configCommand;
            Parameters = parameters;
        }

        public ConfigCommand ConfigCommand { get; }

        public string Parameters { get; }
    }
}

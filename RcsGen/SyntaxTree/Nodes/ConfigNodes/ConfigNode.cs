namespace RcsGen.SyntaxTree.Nodes
{
    internal abstract class ConfigNode : Node
    {
        protected ConfigNode(ConfigCommand configCommand) : base(NodeType.Config)
        {
            ConfigCommand = configCommand;
        }

        public ConfigCommand ConfigCommand { get; }
    }
}

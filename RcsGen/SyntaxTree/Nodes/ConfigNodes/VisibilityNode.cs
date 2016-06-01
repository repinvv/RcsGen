namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    internal class VisibilityNode : ConfigNode
    {
        public VisibilityNode(string visibility) : base(ConfigCommand.Visibility)
        {
            Visibility = visibility;
        }

        public string Visibility { get; }
    }
}

namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    internal class PartialPatternNode : ConfigNode
    {
        public string Pattern { get; set; }

        public PartialPatternNode(string pattern) : base(ConfigCommand.PartialPattern)
        {
            Pattern = pattern;
        }
    }
}

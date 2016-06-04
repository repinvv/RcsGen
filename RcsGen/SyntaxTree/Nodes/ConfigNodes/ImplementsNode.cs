namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    using System.Collections.Generic;

    internal class ImplementsNode : ConfigNode
    {
        public ImplementsNode(List<string> interfaces) : base(ConfigCommand.Implements)
        {
            Interfaces = interfaces;
        }

        public List<string> Interfaces { get; }
    }
}

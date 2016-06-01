namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    using System.Collections.Generic;

    internal class UsingNode : ConfigNode
    {
        public UsingNode(List<string> usings) : base(ConfigCommand.Using)
        {
            Usings = usings;
        }

        public List<string> Usings { get; }
    }
}

namespace RcsGen.SyntaxTree.Nodes.ConfigNodes
{
    internal class MemberNode : ConfigNode
    {
        public MemberNode(string member) : base(ConfigCommand.Member)
        {
            Member = member;
        }

        public string Member { get; }
    }
}

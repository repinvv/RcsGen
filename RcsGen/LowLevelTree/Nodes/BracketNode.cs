namespace RcsGen.LowLevelTree.Nodes
{
    internal abstract class BracketNode : ContainerNode
    {
        public override NodeType NodeType => NodeType.Bracket;

        public abstract char Opening { get; }

        public abstract char Closing { get; }
    }
}

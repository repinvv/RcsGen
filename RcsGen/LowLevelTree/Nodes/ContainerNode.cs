namespace RcsGen.LowLevelTree.Nodes
{
    using System.Collections.Generic;

    internal abstract class ContainerNode : INode
    {
        public abstract NodeType NodeType { get; }

        public List<INode> Nodes { get; set; }
    }
}

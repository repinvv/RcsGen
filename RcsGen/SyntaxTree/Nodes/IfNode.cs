namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class IfNode : INode
    {
        public NodeType NodeType => NodeType.If;

        public string Condition { get; set; }

        public IEnumerable<INode> IfNodes { get; set; }

        public IEnumerable<INode> ElseNodes { get; set; }
    }
}

namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class ForNode : INode
    {
        public NodeType NodeType => NodeType.For;

        public string Keyword { get; set; }

        public string CycleDefinition { get; set; }

        public IEnumerable<INode> ChildNodes { get; set; }
    }
}

namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class ForNode : Node
    {
        public ForNode(string keyword) : base(NodeType.For)
        {
            Keyword = keyword;
        }

        public string Keyword { get; }

        public string Condition { get; set; }

        public IEnumerable<Node> ChildNodes { get; set; }
    }
}

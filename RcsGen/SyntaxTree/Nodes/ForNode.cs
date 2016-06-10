namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class ForNode : Node
    {
        public ForNode(string keyword, string condition, NodeStore childNodes) : base(NodeType.For)
        {
            Keyword = keyword;
            Condition = condition;
            ChildNodes = childNodes;
        }

        public string Keyword { get; }

        public string Condition { get; }

        public NodeStore ChildNodes { get; }
    }
}

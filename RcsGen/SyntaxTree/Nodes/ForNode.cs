namespace RcsGen.SyntaxTree.Nodes
{
    using System.Collections.Generic;

    internal class ForNode : Node
    {
        public ForNode(string keyword, string condition, List<Node> childNodes) : base(NodeType.For)
        {
            Keyword = keyword;
            Condition = condition;
            ChildNodes = childNodes;
        }

        public string Keyword { get; }

        public string Condition { get; }

        public List<Node> ChildNodes { get; }
    }
}

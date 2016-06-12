namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class NodeStore
    {
        private bool lineStart = true;
        private bool spaces;
        private readonly List<Node> nodes = new List<Node>();

        public IReadOnlyList<Node> Nodes => nodes;
        public bool LineStart => lineStart;

        public void Add(Node node, bool removeSpace = false)
        {
            if (spaces && removeSpace)
            {
                nodes.RemoveAt(nodes.Count - 1);
            }

            spaces = lineStart &&
                     node.NodeType == NodeType.Literal &&
                     string.IsNullOrWhiteSpace(((ContentNode)node).Content);
            lineStart = node.NodeType == NodeType.Eol || node.NodeType == NodeType.ForceEol;
            nodes.Add(node);
        }
    }
}

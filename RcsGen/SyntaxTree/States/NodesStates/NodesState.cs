namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;

    internal abstract class NodesState : AccumulatingState
    {
        protected readonly List<Node> nodes;

        protected NodesState(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        protected void AddAccumulated()
        {
            var current = Accumulated;
            Clear();

            if (current != string.Empty)
            {
                nodes.Add(new ContentNode(current, NodeType.Literal));
            }
        }

        protected void AddAccumulatedWithEol(List<Node> list)
        {
            var current = Accumulated;
            Clear();
            if (!string.IsNullOrWhiteSpace(current))
            {
                nodes.Add(new ContentNode(current, NodeType.Literal));
                nodes.Add(new Node(NodeType.Eol));
                return;
            }

            if (!NodesHaveContent(nodes))
            {
                return;
            }

            if (current != string.Empty)
            {
                nodes.Add(new ContentNode(current, NodeType.Literal));
            }

            nodes.Add(new Node(NodeType.Eol));
        }

        private bool NodesHaveContent(List<Node> list)
        {
            return nodes.Any() 
                && nodes.Last().NodeType != NodeType.Eol 
                && nodes.Last().NodeType != NodeType.Config;
        }
    }
}

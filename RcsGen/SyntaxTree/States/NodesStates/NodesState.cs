namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;

    internal abstract class NodesState : AccumulatingState
    {
        protected readonly NodeStore nodes;
        private readonly IState stateToFinish;

        protected NodesState(NodeStore nodes, IState stateToFinish)
        {
            this.nodes = nodes;
            this.stateToFinish = stateToFinish;
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

        protected void AddAccumulatedWithEol()
        {
            var current = Accumulated;
            Clear();
            if (!string.IsNullOrWhiteSpace(current))
            {
                nodes.Add(new ContentNode(current, NodeType.Literal));
                nodes.Add(new Node(NodeType.Eol));
                return;
            }

            if (!NodesHaveContent())
            {
                return;
            }

            if (current != string.Empty)
            {
                nodes.Add(new ContentNode(current, NodeType.Literal));
            }

            nodes.Add(new Node(NodeType.Eol));
        }

        private bool NodesHaveContent()
        {
            return nodes.Nodes.Any() 
                && nodes.Nodes.Last().NodeType != NodeType.Eol
                && nodes.Nodes.Last().NodeType != NodeType.ForceEol
                && nodes.Nodes.Last().NodeType != NodeType.Config;
        }

        public override void Finish()
        {
            AddAccumulated();
            stateToFinish?.Finish();
        }
    }
}

namespace RcsGen.SyntaxTree.States.NodesStates
{
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
            AddAccumulated();
            if (!HaveContent())
            {
                return;
            }

            var last = nodes.Nodes.Last();
            if (last.NodeType != NodeType.ForceEol
                && last.NodeType != NodeType.Config)
            {
                nodes.Add(new Node(NodeType.Eol));
            }
        }

        protected bool HaveContent()
        {
            return nodes.Nodes.Any(x => x.NodeType != NodeType.Config);
        }

        public override void Finish()
        {
            AddAccumulated();
            stateToFinish?.Finish();
        }
    }
}

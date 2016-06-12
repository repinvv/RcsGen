﻿namespace RcsGen.SyntaxTree.States.NodesStates
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
            AddAccumulated();
            if (!nodes.Nodes.Any())
            {
                return;
            }

            var last = nodes.Nodes.Last();
            if (last.NodeType != NodeType.Eol
                && last.NodeType != NodeType.ForceEol
                && last.NodeType != NodeType.Config)
            {
                nodes.Add(new Node(NodeType.Eol));
            }
        }

        public override void Finish()
        {
            AddAccumulated();
            stateToFinish?.Finish();
        }
    }
}

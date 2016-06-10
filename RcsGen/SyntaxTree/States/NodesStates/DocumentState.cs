﻿namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class DocumentState : NodesState
    {
        private readonly StateMachine stateMachine;

        public DocumentState(StateMachine stateMachine, List<Node> nodes) : base(nodes)
        {
            this.stateMachine = stateMachine;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    AddAccumulatedWithEol(nodes);
                    break;
                case "@":
                    AddAccumulated();
                    var allConfig = nodes.All(x => x.NodeType == NodeType.Config);
                    stateMachine.State = allConfig
                        ? (IState)new AtConfigState(stateMachine, this, nodes)
                        : new AtState(stateMachine, this, nodes);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }
    }
}

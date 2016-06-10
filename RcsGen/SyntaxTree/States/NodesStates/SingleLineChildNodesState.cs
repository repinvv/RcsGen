namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class SingleLineChildNodesState : NodesState
    {
        private readonly StateMachine stateMachine;

        public SingleLineChildNodesState(StateMachine stateMachine, List<Node> nodes) : base(nodes)
        {
            this.stateMachine = stateMachine;
        }

        public Action ReturnAction { get; set; }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "@":
                    AddAccumulated();
                    stateMachine.State = new AtState(stateMachine, this, nodes);
                    break;
                case "}":
                    AddAccumulated();
                    ReturnAction();
                    break;
                case "\n":
                    AddAccumulatedWithEol(nodes);
                    stateMachine.State = new MultiLineChildNodesState(stateMachine, nodes, ReturnAction);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }
    }
}

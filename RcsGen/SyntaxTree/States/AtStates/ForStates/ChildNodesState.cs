namespace RcsGen.SyntaxTree.States.AtStates.ForStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class ChildNodesState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly List<Node> nodes;

        public ChildNodesState(StateMachine stateMachine, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.nodes = nodes;
        }

        public Action ReturnAction { get; set; }

        public override void ProcessToken(string token)
        {
            if (token == "}")
            {
                ReturnAction();
                return;
            }

            nodes.Add(new ContentNode(token, NodeType.Literal));
        }
    }
}

namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class MultiLineChildNodesState : NodesState
    {
        private readonly StateMachine stateMachine;
        private readonly Action returnAction;
        int lastEol;

        public MultiLineChildNodesState(StateMachine stateMachine, List<Node> nodes, Action returnAction) 
            : base(nodes)
        {
            this.stateMachine = stateMachine;
            this.returnAction = returnAction;
            lastEol = nodes.Count;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "@":
                    AddAccumulated();
                    stateMachine.State = new AtState(nodes, stateMachine, this);
                    break;
                case "}":
                    if (OnlySpaces())
                    {
                        returnAction();
                    }
                    else
                    {
                        Accumulate(token);
                    }
                    
                    break;
                case "\n":
                    AddAccumulatedWithEol(nodes);
                    lastEol = nodes.Count;
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        private bool OnlySpaces()
        {
            return string.IsNullOrWhiteSpace(Accumulated) && nodes.Count == lastEol;
        }
    }
}

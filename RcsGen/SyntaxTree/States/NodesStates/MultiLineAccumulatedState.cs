namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class MultiLineChildNodesState : NodesState
    {
        private readonly StateMachine stateMachine;
        private readonly Action returnAction;

        public MultiLineChildNodesState(StateMachine stateMachine, 
            NodeStore nodes,
            Action returnAction,
            IState stateToFinish) 
            : base(nodes, stateToFinish)
        {
            this.stateMachine = stateMachine;
            this.returnAction = returnAction;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "@":
                    AddAccumulated();
                    stateMachine.State = new AtState(stateMachine, this, nodes);
                    break;
                case "}":
                    if (string.IsNullOrWhiteSpace(Accumulated) && nodes.LineStart)
                    {
                        returnAction();
                    }
                    else
                    {
                        Accumulate(token);
                    }
                    
                    break;
                case "\n":
                    AddAccumulatedWithEol();
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }
    }
}

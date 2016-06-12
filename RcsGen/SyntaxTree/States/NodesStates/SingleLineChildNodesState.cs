namespace RcsGen.SyntaxTree.States.NodesStates
{
    using System;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class SingleLineChildNodesState : NodesState
    {
        private readonly StateMachine stateMachine;
        private readonly IState stateToFinish;

        public SingleLineChildNodesState(StateMachine stateMachine,
            NodeStore nodes,
            IState stateToFinish) 
            : base(nodes, stateToFinish)
        {
            this.stateMachine = stateMachine;
            this.stateToFinish = stateToFinish;
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
                    AddAccumulatedWithEol();
                    stateMachine
                        .State = new MultiLineChildNodesState(stateMachine,
                                                              nodes,
                                                              ReturnAction,
                                                              stateToFinish);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }
    }
}

namespace RcsGen.SyntaxTree.States.NodesStates
{
    using RcsGen.SyntaxTree.States.AtStates;

    internal class DocumentState : NodesState
    {
        private readonly StateMachine stateMachine;

        public DocumentState(StateMachine stateMachine, NodeStore nodes, IState stateToFinish) 
            : base(nodes, stateToFinish)
        {
            this.stateMachine = stateMachine;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    AddAccumulatedWithEol();
                    break;
                case "@":
                    AddAccumulated();
                    stateMachine.State = HaveContent()
                        ? new AtState(stateMachine, this, nodes)
                        : (IState)new AtConfigState(stateMachine, this, nodes);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }
    }
}

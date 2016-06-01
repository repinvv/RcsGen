namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    internal class SkipSpacesState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState state;

        public SkipSpacesState(StateMachine stateMachine, IState state)
        {
            this.stateMachine = stateMachine;
            this.state = state;
        }

        public void ProcessToken(string token)
        {
            if (token == " " || token == "\t")
            {
                return;
            }

            stateMachine.State = state;
            state.ProcessToken(token);
        }

        public void Finish() { }
    }
}

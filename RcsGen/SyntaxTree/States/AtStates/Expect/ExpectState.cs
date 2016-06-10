namespace RcsGen.SyntaxTree.States.AtStates
{
    internal class ExpectState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState rejectState;
        private readonly string expecting;

        public ExpectState(StateMachine stateMachine, IState rejectState, string expecting)
        {
            this.stateMachine = stateMachine;
            this.rejectState = rejectState;
            this.expecting = expecting;
        }

        public IState SuccessState { get; set; }

        public override void ProcessToken(string token)
        {
            if (token == expecting)
            {
                stateMachine.State = SuccessState;
                return;
            }

            switch (token)
            {
                case " ":
                case "\n":
                case "\t":
                    Accumulate(token);
                    break;
                default:
                    Accumulate(token);
                    Finish();
                    break;
            }
        }

        public override void Finish()
        {
            stateMachine.State = rejectState;
            foreach (var t in tokens)
            {
                stateMachine.ProcessToken(t);
            }
        }
    }
}

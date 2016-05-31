namespace RcsGen.SyntaxTree.States.AtStates
{
    internal static class ExpectExtension
    {
        public static ExpectState Expect(this StateMachine stateMachine, string expecting, IState rejectState)
        {
            var expectState = new ExpectState(stateMachine, rejectState, expecting);
            stateMachine.State = expectState;
            return expectState;
        }
    }

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
                    stateMachine.State = rejectState;
                    foreach (var t in tokens)
                    {
                        stateMachine.ProcessToken(t);
                    }
                    break;
            }
        }
    }
}

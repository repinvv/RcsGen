namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class QuotesBracketState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IAccumulatingState previous;
        private bool escaped;

        public QuotesBracketState(StateMachine stateMachine, IAccumulatingState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            Accumulate(token);
            if (escaped)
            {
                escaped = false;
                return;
            }

            switch (token)
            {
                case "\\":
                    escaped = true;
                    break;
                case "\"":
                    stateMachine.State = previous;
                    break;
            }
        }

        public void Finish() => previous.Finish();

        public void Accumulate(string token) => previous.Accumulate(token);
    }
}

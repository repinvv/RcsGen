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
            Accumulate(ch);
            if (escaped)
            {
                escaped = false;
                return;
            }

            switch (ch)
            {
                case '\\':
                    escaped = true;
                    break;
                case '"':
                    stateMachine.State = previous;
                    break;
            }
        }

        public void Accumulate(string token) => previous.Accumulate(token);
    }
}

namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class AtBracketState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IAccumulatingState previous;

        public AtBracketState(StateMachine stateMachine, IAccumulatingState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            if (token == "\"")
            {
                stateMachine.State = new UnescapedQuotesBracketState(stateMachine, previous);
                Accumulate(token);
            }
            else
            {
                stateMachine.State = previous;
                previous.ProcessToken(token);
            }
        }

        public void Accumulate(string token) => previous.Accumulate(token);
    }
}

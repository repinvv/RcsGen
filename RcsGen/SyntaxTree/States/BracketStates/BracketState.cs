namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal abstract class BracketState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IAccumulatingState previous;
        private readonly string closing;
        private readonly BracketStateFactory factory;

        protected BracketState(StateMachine stateMachine, 
            IAccumulatingState previous, string closing, params string[] allowed)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.closing = closing;
            factory = new BracketStateFactory(stateMachine, this, allowed);
        }

        public virtual void ProcessToken(string token)
        {
            Accumulate(token);
            if (token == closing)
            {
                stateMachine.State = previous;
            }
            else
            {
                factory.TryBracket(token);
            }
        }

        public void Finish() => previous.Finish();

        public void Accumulate(string token) => previous.Accumulate(token);
    }
}

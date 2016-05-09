namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal abstract class BracketState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IAccumulatingState previous;
        private readonly char closing;
        private readonly BracketStateFactory factory;

        protected BracketState(StateMachine stateMachine, 
            IAccumulatingState previous, char closing, params char[] allowed)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.closing = closing;
            factory = new BracketStateFactory(stateMachine, this, allowed);
        }

        public virtual void ProcessChar(char ch)
        {
            Accumulate(ch);
            if (ch == closing)
            {
                stateMachine.State = previous;
            }
            else
            {
                factory.TryBracket(ch);
            }
        }
        

        public void Accumulate(char ch) => previous.Accumulate(ch);
    }
}

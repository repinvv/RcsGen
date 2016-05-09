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

        public void ProcessChar(char ch)
        {
            if (ch == '"')
            {
                stateMachine.State = new UnescapedQuotesBracketState(stateMachine, previous);
                Accumulate(ch);
            }
            else
            {
                stateMachine.State = previous;
                previous.ProcessChar(ch);
            }
        }

        public void Accumulate(char ch) => previous.Accumulate(ch);
    }
}

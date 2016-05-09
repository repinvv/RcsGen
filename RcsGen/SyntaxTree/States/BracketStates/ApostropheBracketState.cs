namespace RcsGen.SyntaxTree.States.BracketStates
{
    internal class ApostropheBracketState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IAccumulatingState previous;
        private bool escaped;

        public ApostropheBracketState(StateMachine stateMachine, IAccumulatingState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessChar(char ch)
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
                case '\'':
                    stateMachine.State = previous;
                    break;
            }
        }

        public void Accumulate(char ch) => previous.Accumulate(ch);

    }
}

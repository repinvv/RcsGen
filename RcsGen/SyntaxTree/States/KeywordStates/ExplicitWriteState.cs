namespace RcsGen.SyntaxTree.States.KeywordStates
{
    internal class ExplicitWriteState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public ExplicitWriteState(StateMachine stateMachine, IState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessChar(char ch)
        { }
    }
}

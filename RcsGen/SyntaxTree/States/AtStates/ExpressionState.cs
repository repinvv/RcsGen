namespace RcsGen.SyntaxTree.States.ExpressionStates
{
    internal class ExpressionState : IState
    {
        private IState previous;
        private StateMachine stateMachine;

        public ExpressionState(StateMachine stateMachine, IState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        { }
    }
}

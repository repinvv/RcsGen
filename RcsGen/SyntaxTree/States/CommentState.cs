namespace RcsGen.SyntaxTree.States
{
    internal class CommentState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        bool starred;

        public CommentState(StateMachine stateMachine, IState previous)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessToken(string token)
        {
            switch (token)
            {
                case "*":
                    starred = true;
                    break;
                case "@":
                    if (starred)
                    {
                        stateMachine.State = previous;
                    }

                    break;
                default:
                    starred = false;
                    break;
            }
        }

        public void Finish() => previous.Finish();
    }
}

namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;

    internal class Unexpected : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState reject;
        private readonly string[] unexpected;

        public Unexpected(StateMachine stateMachine, IState reject, params string[] unexpected)
        {
            this.stateMachine = stateMachine;
            this.reject = reject;
            this.unexpected = unexpected;
        }

        public IState State { get; set; }

        public void ProcessToken(string token)
        {
            if (unexpected.Contains(token))
            {
                stateMachine.State = reject;
            }
            else
            {
                State.ProcessToken(token);
            }
        }

        public void Finish() => reject.Finish();
    }
}

namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ContentState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly string closing;
        private readonly Action<string> close;
        private readonly IState closingState;
        private readonly BracketStateFactory factory;

        public ContentState(StateMachine stateMachine, 
            string closing, 
            Action<string> close, 
            IState closingState, 
            params string[] allowed)
        {
            this.stateMachine = stateMachine;
            this.closing = closing;
            this.close = close;
            this.closingState = closingState;
            factory = new BracketStateFactory(stateMachine, this, allowed);
        }

        public override void ProcessToken(string token)
        {
            if (token == closing)
            {
                stateMachine.State = closingState;
                close(Accumulated);
                return;
            }

            Accumulate(token);
            factory.TryBracket(token);
        }

        public override void Finish()
        {
            close(Accumulated);
            closingState.Finish();
        }
    }
}

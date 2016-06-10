namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ContentState : AccumulatingState
    {
        private readonly string closing;
        private readonly Action<string> close;
        private readonly BracketStateFactory factory;

        public ContentState(StateMachine stateMachine, string closing, Action<string> close, params string[] allowed)
        {
            this.closing = closing;
            this.close = close;
            factory = new BracketStateFactory(stateMachine, this, allowed);
        }

        public override void ProcessToken(string token)
        {
            if (token == closing)
            {
                close(Accumulated);
                return;
            }

            Accumulate(token);
            factory.TryBracket(token);
        }

        public override void Finish()
        {
            close(Accumulated);
        }
    }
}

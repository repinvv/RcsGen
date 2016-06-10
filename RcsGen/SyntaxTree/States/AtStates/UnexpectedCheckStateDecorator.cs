namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;

    internal class UnexpectedCheckStateDecorator : IState
    {
        private readonly IState state;
        private readonly Action reject;
        private readonly string[] unexpected;

        public UnexpectedCheckStateDecorator(IState state, Action reject, params string[] unexpected)
        {
            this.state = state;
            this.reject = reject;
            this.unexpected = unexpected;
        }

        public void ProcessToken(string token)
        {
            if (unexpected.Contains(token))
            {
                reject();
            }
            else
            {
                state.ProcessToken(token);
            }
        }

        public void Finish() => state.Finish();
    }
}

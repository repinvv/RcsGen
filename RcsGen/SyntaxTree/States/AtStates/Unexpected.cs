namespace RcsGen.SyntaxTree.States.AtStates
{
    using System;
    using System.Linq;

    internal class Unexpected : IState
    {
        private readonly Action reject;
        private readonly string[] unexpected;

        public Unexpected(Action reject, params string[] unexpected)
        {
            this.reject = reject;
            this.unexpected = unexpected;
        }

        public IState State { get; set; }

        public void ProcessToken(string token)
        {
            if (unexpected.Contains(token))
            {
                reject();
            }
            else
            {
                State.ProcessToken(token);
            }
        }

        public void Finish() => State.Finish();
    }
}

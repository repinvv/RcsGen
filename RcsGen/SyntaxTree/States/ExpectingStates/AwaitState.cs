namespace RcsGen.SyntaxTree.States.ExpectingStates
{
    using System;

    internal class AwaitState : ExpectState
    {
        public AwaitState(StateMachine stateMachine, string value, Action setSuccessState) 
            : base(stateMachine, value, setSuccessState)
        { }

        protected override void Unexpected(char ch, int index)
        {
            if (index == 0 && IsBlank(ch))
            {
                return;
            }

            base.Unexpected(ch, index);
        }

        private bool IsBlank(char ch)
        {
            switch (ch)
            {
                case ' ':
                case '\r':
                case '\n':
                    return true;
                default:
                    return false;
            }
        }
    }
}

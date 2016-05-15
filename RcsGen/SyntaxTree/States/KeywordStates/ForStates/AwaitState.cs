namespace RcsGen.SyntaxTree.States.KeywordStates.ForStates
{
    using System.Collections.Generic;

    internal class AwaitState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState successState;
        private readonly IState rejectState;
        private readonly string awaiting;
        private readonly List<char> all = new List<char>();
        private int foundIndex = 0;

        public AwaitState(StateMachine stateMachine, IState successState, IState rejectState, string awaiting)
        {
            this.stateMachine = stateMachine;
            this.successState = successState;
            this.rejectState = rejectState;
            this.awaiting = awaiting;
        }

        public override void ProcessChar(char ch)
        {
            all.Add(ch);
            if (awaiting[foundIndex] == ch)
            {
                foundIndex++;
                if (foundIndex == awaiting.Length)
                {
                    stateMachine.State = successState;
                }
                return;
            }

            if (foundIndex == 0 && IsBlank(ch))
            {
                return;
            }

            stateMachine.State = rejectState;
            all.ForEach(x => stateMachine.ProcessChar(x));
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

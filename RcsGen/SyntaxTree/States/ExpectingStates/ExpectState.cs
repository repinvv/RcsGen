namespace RcsGen.SyntaxTree.States.ExpectingStates
{
    using System;
    using System.Collections.Generic;

    internal class ExpectState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly string value;
        private readonly Action setSuccessState;

        private readonly List<char> symbols = new List<char>();
        private int foundIndex;
        
        public Func<IState> GetRejectState { private get; set; }

        public ExpectState(StateMachine stateMachine, string value, Action setSuccessState)
        {
            this.stateMachine = stateMachine;
            this.value = value;
            this.setSuccessState = setSuccessState;
        }

        public void ProcessChar(char ch)
        {
            symbols.Add(ch);
            if (value[foundIndex] != ch)
            {
                Unexpected(ch, foundIndex);
            }

            foundIndex++;

            if (foundIndex == value.Length)
            {
                setSuccessState();
            }
        }

        protected virtual void Unexpected(char ch, int index)
        {
            stateMachine.State = GetRejectState();
            symbols.ForEach(x => stateMachine.ProcessChar(x));
        }
    }
}

namespace RcsGen.SyntaxTree.States.ExpectingStates
{
    using System;

    internal class Chain : ICanChain
    {
        private readonly StateMachine stateMachine;

        public Chain(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public ICanChainAndReject Expect(string value, Func<IState> successFunc)
        {
            var expectState = new ExpectState(stateMachine, value, () => stateMachine.State = successFunc());
            stateMachine.State = expectState;
            return new ChainElementLink(stateMachine, expectState, this);
        }

        public ICanChainAndReject Expect(string value, Action<ICanChain> successAction)
        {
            var expectState = new ExpectState(stateMachine, value, () => successAction(this));
            stateMachine.State = expectState;
            return new ChainElementLink(stateMachine, expectState, this);
        }

        public ICanChainAndReject Await(string value, Func<IState> successFunc)
        {
            var expectState = new AwaitState(stateMachine, value, () => stateMachine.State = successFunc());
            stateMachine.State = expectState;
            return new ChainElementLink(stateMachine, expectState, this);
        }

        public ICanChainAndReject Await(string value, Action<ICanChain> successAction)
        {
            var expectState = new AwaitState(stateMachine, value, () => successAction(this));
            stateMachine.State = expectState;
            return new ChainElementLink(stateMachine, expectState, this);
        }
    }
}

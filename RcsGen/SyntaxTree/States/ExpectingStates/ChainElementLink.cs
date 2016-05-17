namespace RcsGen.SyntaxTree.States.ExpectingStates
{
    using System;

    internal class ChainElementLink : ICanChainAndReject
    {
        private readonly StateMachine stateMachine;
        private readonly ExpectState expectState;
        private readonly Chain chain;

        public ChainElementLink(StateMachine stateMachine, ExpectState expectState, Chain chain)
        {
            this.stateMachine = stateMachine;
            this.expectState = expectState;
            this.chain = chain;
        }

        public ICanChainAndReject Expect(string value, Func<IState> successFunc)
        {
            var nextExpectState = new ExpectState(stateMachine, value, () => stateMachine.State = successFunc());
            expectState.GetRejectState = () => nextExpectState;
            return new ChainElementLink(stateMachine, nextExpectState, chain);
        }

        public ICanChainAndReject Expect(string value, Action<ICanChain> successAction)
        {
            var nextExpectState = new ExpectState(stateMachine, value, () => successAction(chain));
            expectState.GetRejectState = () => nextExpectState;
            return new ChainElementLink(stateMachine, expectState, chain);
        }

        public ICanChainAndReject Await(string value, Func<IState> successFunc)
        {
            var nextExpectState = new AwaitState(stateMachine, value, () => stateMachine.State = successFunc());
            expectState.GetRejectState = () => nextExpectState;
            return new ChainElementLink(stateMachine, nextExpectState, chain);
        }

        public ICanChainAndReject Await(string value, Action<ICanChain> successAction)
        {
            var nextExpectState = new AwaitState(stateMachine, value, () => successAction(chain));
            expectState.GetRejectState = () => nextExpectState;
            return new ChainElementLink(stateMachine, expectState, chain);
        }

        public void Otherwize(Func<IState> reject)
        {
            expectState.GetRejectState = reject;
        }
    }
}

namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.ExpectingStates;

    internal class KeywordStateFactory
    {
        private readonly StateMachine stateMachine;
        private readonly Func<IState> reject;
        private readonly IState previous;

        public KeywordStateFactory(StateMachine stateMachine, Func<IState> reject, IState previous)
        {
            this.stateMachine = stateMachine;
            this.reject = reject;
            this.previous = previous;
        }

        public void SetupAllKeywordsChain(List<Node> nodes)
        {
            var chain = new Chain(stateMachine)
                .Expect(KeywordConstants.Config.Inherits,
                        () => new GotConfigState(ConfigCommand.Inherits, nodes, stateMachine, previous))
                .Expect(KeywordConstants.Config.Using,
                        () => new GotConfigState(ConfigCommand.Using, nodes, stateMachine, previous))
                .Expect(KeywordConstants.Config.Visibility,
                        () => new GotConfigState(ConfigCommand.Visibility, nodes, stateMachine, previous));
            SetupKeywords(nodes, chain);
        }

        public void SetupKeywordsChain(List<Node> nodes)
        {
            SetupKeywords(nodes, new Chain(stateMachine));
        }

        private void SetupKeywords(List<Node> nodes, ICanChain chain)
        {
            chain
                .Expect(KeywordConstants.If, c => { })
                .Otherwize(reject);
        }
    }
}

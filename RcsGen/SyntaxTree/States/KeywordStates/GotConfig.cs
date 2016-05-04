namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class GotConfigState : IState
    {
        private readonly ConfigCommand command;
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public GotConfigState(ConfigCommand command, List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.command = command;
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessChar(char ch)
        { }
    }
}

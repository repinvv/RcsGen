namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class ImplementsState : AccumulatingState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public ImplementsState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    CreateNode();
                    stateMachine.State = previous;
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public void CreateNode()
        {
            var usingParams = Accumulated
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (usingParams.Any())
            {
                nodes.Add(new ImplementsNode(usingParams));
            }
        }

        public override void Finish() { }
    }
}

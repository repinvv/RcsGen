namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal class InheritsState : AccumulatingState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public InheritsState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            if (token == "\n")
            {
                stateMachine.State = previous;
                return;
            }

            if (token != " ")
            {
                Accumulate(token);
                return;
            }

            var emptyList = new List<Tuple<string, string>>();
            stateMachine.State =
                new InheritsParameterTypeState(stateMachine, previous, emptyList, Accumulated, nodes);
        }

        public override void Finish() { }
    }
}

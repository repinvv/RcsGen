namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class InheritsState : AccumulatingState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public InheritsState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        private void CreateNode(string content)
            => nodes.Add(new InheritsNode(Accumulated, content.CreateParameters()));

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    var baseClass = Accumulated;
                    if (baseClass != string.Empty)
                    {
                        nodes.Add(new InheritsNode(baseClass));
                    }
                    stateMachine.State = previous;

                    break;
                case "(":
                    stateMachine.State = CreateContentState();
                    break;
                case " ":
                    stateMachine
                        .ExpectAtSameLine("(", previous)
                        .SuccessState = CreateContentState();
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        private IState CreateContentState()
            => new Unexpected(() => stateMachine.State = previous, "\n")
               {
                   State = new ContentState(stateMachine, ")", CreateNode, previous)
               };

        public override void Finish() { }
    }
}

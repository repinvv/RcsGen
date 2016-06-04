namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System.Collections.Generic;
    using System.Reflection;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class MemberState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;
        private bool opened;
        private readonly BracketStateFactory factory;

        public MemberState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
            factory = new BracketStateFactory(stateMachine, this, BracketStateFactory.AllBrackets);
        }

        public override void ProcessToken(string token)
        {
            if (token != "{" && !opened)
            {
                stateMachine.State = previous;
                previous.ProcessToken(token);
                return;
            }

            switch (token)
            {
                case "{":
                    opened = true;
                    break;
                case "}":
                    nodes.Add(new MemberNode(Accumulated));
                    stateMachine.ExpectAtSameLine("\n", previous)
                                .SuccessState = previous;
                    break;
                default:
                    Accumulate(token);
                    factory.TryBracket(token);
                    break;
            }
        }

        public override void Finish() { }
    }
}

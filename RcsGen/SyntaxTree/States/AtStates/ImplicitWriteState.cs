namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ImplicitWriteState : AccumulatingState
    {
        protected readonly List<Node> nodes;
        protected readonly StateMachine stateMachine;
        protected readonly IState previous;
        private BracketStateFactory factory;

        public ImplicitWriteState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            factory = new BracketStateFactory(stateMachine, this, "(", "<");
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "@":
                case "\"":
                case ")":
                case "}":
                case "]":
                case "'":
                case " ":
                case ">":
                case ";":
                case ",":
                case "\t":
                case "=":
                    Finish();
                    stateMachine.State = previous;
                    previous.ProcessToken(token);
                    break;
                case "\n":
                    Finish();
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    break;
                default:
                    Accumulate(token);
                    factory.TryBracket(token);
                    break;
            }


        }

        public override void Finish()
        {
            nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));
        }
    }
}

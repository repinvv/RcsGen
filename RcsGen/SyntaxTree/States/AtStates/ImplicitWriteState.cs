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

        public ImplicitWriteState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
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
                    Finish();
                    stateMachine.State = previous;
                    previous.ProcessToken(token);
                    break;
                case "(":
                    stateMachine.State = new RoundParenthesisState(stateMachine, this);
                    Accumulate(token);
                    break;
                case "\n":
                    Finish();
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    break;
                case "<":
                    stateMachine.State = new GenericBracketState(stateMachine, this);
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public override void Finish()
        {
            nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));
        }
    }
}

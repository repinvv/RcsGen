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
            switch (ch)
            {
                case '"':
                case ')':
                case '}':
                case ']':
                case '\'':
                case ' ':
                case '>':
                    nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));
                    stateMachine.State = previous;
                    previous.ProcessToken(token);
                    return;
                case '(':
                    stateMachine.State = new RoundParenthesisState(stateMachine, this);
                    Accumulate(ch);
                    return;
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    return;
                case '<':
                    stateMachine.State = new GenericBracketState(stateMachine, this);
                    return;
                default:
                    Accumulate(ch);
                    return;
            }
        }
    }
}

namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class ExplicitWriteState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;
        readonly List<char> symbols = new List<char>();

        public ExplicitWriteState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
        }

        public void ProcessChar(char ch)
        {
            switch (ch)
            {
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.CurrentState = previous;
                    return;
                case ')':
                    nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                    stateMachine.CurrentState = previous;
                    return;
                default:
                    symbols.Add(ch);
                    return;
            }
        }
    }
}

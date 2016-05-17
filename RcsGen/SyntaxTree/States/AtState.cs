namespace RcsGen.SyntaxTree.States
{
    using System;
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.ExpressionStates;
    using RcsGen.SyntaxTree.States.KeywordStates;

    internal class AtState : IState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public AtState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessChar(char ch)
        {
            switch (ch)
            {
                case '@': 
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    stateMachine.State = previous;
                    return;
                case ' ':
                case '"':
                case '<':
                case '>':
                case '[':
                case ']':
                case ')':
                case '}':
                case '\'':
                    nodes.Add(new ContentNode("@" + ch, NodeType.Literal));
                    stateMachine.State = previous;
                    return;
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    return;
                case '{':
                    stateMachine.State = new ExpressionState(stateMachine, previous);
                    return;
                case '*':
                    stateMachine.State = new CommentState(() => stateMachine.State = previous);
                    return;
                case '(':
                    stateMachine.State = new ExplicitWriteState(stateMachine, previous, nodes);
                    return;
                default:
                    stateMachine.State = new KeywordsState(nodes, stateMachine, previous);
                    stateMachine.ProcessChar(ch);
                    return;
            }
        }
    }
}

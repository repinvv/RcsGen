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
        private readonly bool allKeywords;

        public AtState(List<Node> nodes, StateMachine stateMachine, IState previous, bool allKeywords = false)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.allKeywords = allKeywords;
        }

        public void ProcessChar(char ch)
        {
            switch (ch)
            {
                case '@': 
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    stateMachine.CurrentState = previous;
                    return;
                case ' ':
                    nodes.Add(new ContentNode("@ ", NodeType.Literal));
                    stateMachine.CurrentState = previous;
                    return;
                case '"':
                    return; // todo
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode("@", NodeType.Literal));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.CurrentState = previous;
                    return;
                case '{':
                    stateMachine.CurrentState = new ExpressionState(stateMachine, previous);
                    return;
                case '*':
                    stateMachine.CurrentState = new CommentState(() => stateMachine.CurrentState = previous);
                    return;
                case '(':
                    stateMachine.CurrentState = new ExplicitWriteState(stateMachine, previous);
                default:
                    stateMachine.CurrentState = allKeywords 
                        ? new AllKeywordsState(nodes, stateMachine, previous)
                        : new KeywordsState(nodes, stateMachine, previous);
                    stateMachine.CurrentState.ProcessChar(ch);
                    return;
            }
        }
    }
}

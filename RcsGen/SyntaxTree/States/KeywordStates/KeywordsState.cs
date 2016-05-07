namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class KeywordsState : IState
    {
        protected readonly List<Node> nodes;
        protected readonly StateMachine stateMachine;
        protected readonly IState previous;
        protected readonly List<char> symbols = new List<char>();

        public KeywordsState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public virtual void ProcessChar(char ch)
        {
            switch (ch)
            {
                case ' ':
                    if (ProcessCommandKeywords(new string(symbols.ToArray())))
                    {
                        return;
                    }

                    nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                    stateMachine.CurrentState = previous;
                    previous.ProcessChar(ch);
                    return;
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.CurrentState = previous;
                    return;
                case '(':
                    ProcessCommandKeywords(new string(symbols.ToArray()));
                    return;
                case '<':
                case '"':
                case '\'':
                case '{':
                    return; // todo
                default:
                    symbols.Add(ch);
                    return;
            }
        }

        

        protected bool ProcessCommandKeywords(string keyword)
        {
            switch (keyword)
            {
                case KeywordConstants.If:
                    var ifNode = new IfNode();
                    return true;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    var forNode = new ForNode(keyword);
                    return true;
                default:
                    return false;
            }
        }
    }
}

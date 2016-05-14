namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class KeywordsState : IAccumulatingState
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
            string keyword;
            switch (ch)
            {
                case ' ':
                    keyword = new string(symbols.ToArray());
                    switch (keyword)
                    {
                        case KeywordConstants.If:
                            var ifNode = new IfNode();
                            return;
                        case KeywordConstants.For:
                        case KeywordConstants.Foreach:
                            return;
                        default:
                            nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                            stateMachine.State = previous;
                            previous.ProcessChar(ch);
                            return;
                    }
                case '(':
                    keyword = new string(symbols.ToArray());
                    switch (keyword)
                    {
                        case KeywordConstants.If:
                            var ifNode = new IfNode();
                            return;
                        case KeywordConstants.For:
                        case KeywordConstants.Foreach:
                            return;
                        default:
                            stateMachine.State = new RoundParenthesisState(stateMachine, this);
                            return;
                    }
                case '\r':
                case '\n':
                    nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    return;
                case '<':
                    stateMachine.State = new GenericBracketState(stateMachine, this);
                    return;
                default:
                    symbols.Add(ch);
                    return;
            }
        }

        public void Accumulate(char ch) => symbols.Add(ch);
    }
}

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

        

        protected void ProcessCommandKeywords(string keyword)
        {
            switch (keyword)
            {
                case KeywordConstants.If:
                    var ifNode = new IfNode();
                    return;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    var forNode = new ForNode(keyword);
                    return;
                default:
                    return;
            }
        }
    }
}

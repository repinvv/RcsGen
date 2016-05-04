namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;

    internal class KeywordState : IState
    {
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<char> symbols = new List<char>();

        public KeywordState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public void ProcessChar(char ch)
        {
            string keyword;
            switch (ch)
            {
                case ' ':
                    return;
                case '(':
                    ProcessCommandKeywords(new string(symbols.ToArray()));
                    return;
                default:
                    symbols.Add(ch);
                    return;
            }
        }

        private void ProcessCommandKeywords(string keyword)
        {
            switch (keyword)
            {
                case KeywordConstants.If:
                    var ifNode = new IfNode();
                    nodes.Add(ifNode);
                    return;
                case KeywordConstants.For:
                case KeywordConstants.Foreach:
                    var forNode = new ForNode(keyword);
                    nodes.Add(forNode);
                    return;
                default:
                    return;
            }
        }
    }
}

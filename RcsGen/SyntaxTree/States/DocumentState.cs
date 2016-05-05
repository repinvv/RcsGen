namespace RcsGen.SyntaxTree.States
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;

    internal class DocumentState : IState
    {
        private readonly StateMachine stateMachine;
        private readonly Document document;
        private readonly List<char> symbols = new List<char>();

        public DocumentState(StateMachine stateMachine, Document document)
        {
            this.stateMachine = stateMachine;
            this.document = document;
        }

        public void ProcessChar(char ch)
        {
            switch (ch)
            {
                case '\r':
                case '\n':
                    if (TryAddSymbols())
                    {
                        document.Nodes.Add(new Node(NodeType.Eol));
                    }

                    return;
                case '@':
                    TryAddSymbols();
                    bool allNodesAreConfig = document.Nodes.All(x => x.NodeType == NodeType.Config);
                    stateMachine.CurrentState = new AtState(document.Nodes, stateMachine, this, allNodesAreConfig);
                    return;
                default:
                    symbols.Add(ch);
                    return;
            }
        }

        private bool TryAddSymbols()
        {
            var current = new string(symbols.ToArray());
            symbols.Clear();
            if (string.IsNullOrWhiteSpace(current))
            {
                return false;
            }

            document.Nodes.Add(new LiteralNode(current));
            return true;
        }
    }
}

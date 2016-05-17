namespace RcsGen.SyntaxTree.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.KeywordStates;

    internal class DocumentState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly Document document;

        public DocumentState(StateMachine stateMachine, Document document)
        {
            this.stateMachine = stateMachine;
            this.document = document;
        }

        public override void ProcessChar(char ch)
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
                    Func<IState> reject = () => new AtState(document.Nodes, stateMachine, this);
                    var factory = new KeywordStateFactory(stateMachine, reject, this);
                    if (document.Nodes.All(x => x.NodeType == NodeType.Config))
                    {
                        factory.SetupAllKeywordsChain(document.Nodes);
                    }
                    else
                    {
                        factory.SetupKeywordsChain(document.Nodes);
                    }
                    
                    return;
                default:
                    Accumulate(ch);
                    return;
            }
        }

        private bool TryAddSymbols()
        {
            var current = Accumulated;
            Clear();
            if (string.IsNullOrWhiteSpace(current))
            {
                return false;
            }

            document.Nodes.Add(new ContentNode(current, NodeType.Literal));
            return true;
        }
    }
}

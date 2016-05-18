namespace RcsGen.SyntaxTree.States
{
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.AtStates;

    internal class DocumentState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly Document document;

        public DocumentState(StateMachine stateMachine, Document document)
        {
            this.stateMachine = stateMachine;
            this.document = document;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    if (TryAddAccumulated())
                    {
                        document.Nodes.Add(new Node(NodeType.Eol));
                    }

                    return;
                case "@":
                    TryAddAccumulated();
                    var allConfig = document.Nodes.All(x => x.NodeType == NodeType.Config);
                    stateMachine.State = new AtState(document.Nodes, stateMachine, this, allConfig);
                    return;
                default:
                    Accumulate(token);
                    return;
            }
        }

        private bool TryAddAccumulated()
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

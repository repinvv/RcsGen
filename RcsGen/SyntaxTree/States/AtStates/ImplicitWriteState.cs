namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ImplicitWriteState : AccumulatingState
    {
        protected readonly NodeStore nodes;
        protected readonly StateMachine stateMachine;
        protected readonly IState previous;
        private readonly BracketStateFactory factory;

        public ImplicitWriteState(NodeStore nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
            factory = new BracketStateFactory(stateMachine, this, "(", "<");
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "@":
                case "\"":
                case ")":
                case "}":
                case "]":
                case "'":
                case " ":
                case ">":
                case ";":
                case ",":
                case "\t":
                case "=":
                    CreateNode();
                    stateMachine.State = previous;
                    previous.ProcessToken(token);
                    break;
                case "\n":
                    CreateNode();
                    nodes.Add(new Node(NodeType.Eol));
                    stateMachine.State = previous;
                    break;
                default:
                    Accumulate(token);
                    factory.TryBracket(token);
                    break;
            }
        }

        private void CreateNode() 
            => nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));

        public override void Finish()
        {
            CreateNode();
            previous.Finish();  
        }
    }
}

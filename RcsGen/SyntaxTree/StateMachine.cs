namespace RcsGen.SyntaxTree
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States;
    using RcsGen.SyntaxTree.States.NodesStates;

    internal class StateMachine
    {
        private readonly NodeStore nodes = new NodeStore();

        public StateMachine()
        {
            State = new DocumentState(this, nodes);
        }

        public Document Document => new Document(nodes);

        public IState State { private get; set; }

        public void ProcessToken(string token) => State.ProcessToken(token);

        public void Finish()
        {
            State.Finish();
        }
    }
}

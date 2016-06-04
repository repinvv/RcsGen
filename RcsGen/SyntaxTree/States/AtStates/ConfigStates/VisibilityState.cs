namespace RcsGen.SyntaxTree.States.AtStates.ConfigStates
{
    using System.Collections.Generic;
    using System.Linq;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.Nodes.ConfigNodes;

    internal class VisibilityState : AccumulatingState
    {
        private static readonly string[] visibilityModifiers = new[] { "public", "internal" };
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private readonly IState previous;

        public VisibilityState(List<Node> nodes, StateMachine stateMachine, IState previous)
        {
            this.nodes = nodes;
            this.stateMachine = stateMachine;
            this.previous = previous;
        }

        public override void ProcessToken(string token)
        {
            switch (token)
            {
                case "\n":
                    CreateNode();
                    stateMachine.State = previous;
                    break;
                default:
                    Accumulate(token);
                    break;
            }
        }

        public override void Finish() { }

        public void CreateNode()
        {
            var visibility = Accumulated.Trim().ToLower();
            if (visibilityModifiers.Contains(visibility))
            {
                nodes.Add(new VisibilityNode(visibility));
            }
        }
    }
}

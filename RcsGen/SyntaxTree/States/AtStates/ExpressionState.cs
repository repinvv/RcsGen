namespace RcsGen.SyntaxTree.States.AtStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ExpressionState : AccumulatingState
    {
        private readonly IState previous;
        private readonly List<Node> nodes;
        private readonly StateMachine stateMachine;
        private BracketStateFactory factory;

        public ExpressionState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
            factory = new BracketStateFactory(stateMachine, this, BracketStateFactory.AllBrackets);
        }

        public override void ProcessToken(string token)
        {
            if (token == "}")
            {
                nodes.Add(new ContentNode(Accumulated, NodeType.CodeExpression));
                stateMachine.State = previous;
                return;
            }

            Accumulate(token);
            factory.TryBracket(token);
        }

        public override void Finish()
        { }
    }
}

namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ExplicitWriteState : AccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;
        private readonly BracketStateFactory factory;

        public ExplicitWriteState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
            factory = new BracketStateFactory(stateMachine, this, '<', '(');
        }

        public override void ProcessToken(string token)
        {
            if (ch == ')')
            {
                nodes.Add(new ContentNode(Accumulated, NodeType.WriteExpression));
                stateMachine.State = previous;
            }
            else
            {
                Accumulate(ch);
                factory.TryBracket(ch);
            }
        }
    }
}

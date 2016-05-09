namespace RcsGen.SyntaxTree.States.KeywordStates
{
    using System.Collections.Generic;
    using RcsGen.SyntaxTree.Nodes;
    using RcsGen.SyntaxTree.States.BracketStates;

    internal class ExplicitWriteState : IAccumulatingState
    {
        private readonly StateMachine stateMachine;
        private readonly IState previous;
        private readonly List<Node> nodes;
        readonly List<char> symbols = new List<char>();
        private BracketStateFactory factory;

        public ExplicitWriteState(StateMachine stateMachine, IState previous, List<Node> nodes)
        {
            this.stateMachine = stateMachine;
            this.previous = previous;
            this.nodes = nodes;
            factory = new BracketStateFactory(stateMachine, this, '<', '(');
        }

        public void ProcessChar(char ch)
        {
            if (ch == ')')
            {
                nodes.Add(new ContentNode(new string(symbols.ToArray()), NodeType.WriteExpression));
                stateMachine.State = previous;
            }
            else
            {
                Accumulate(ch);
                factory.TryBracket(ch);
            }
        }

        public void Accumulate(char ch) => symbols.Add(ch);
    }
}

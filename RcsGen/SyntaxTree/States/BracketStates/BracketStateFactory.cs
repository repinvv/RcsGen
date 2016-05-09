namespace RcsGen.SyntaxTree.States.BracketStates
{
    using System.Collections.Generic;
    using System.Linq;

    internal class BracketStateFactory
    {
        private readonly StateMachine stateMachine;
        private readonly IEnumerable<char> allowed;
        private readonly IAccumulatingState state;

        public static readonly char[] AllBrackets = { '<', '(', '{', '"', '\'', '@', '[' };

        public BracketStateFactory(StateMachine stateMachine, IAccumulatingState state, params char[] allowed)
        {
            this.stateMachine = stateMachine;
            this.allowed = allowed;
            this.state = state;
        }

        public bool TryBracket(char ch)
        {
            if (!allowed.Contains(ch))
            {
                return false;
            }

            switch (ch)
            {
                case '<':
                    stateMachine.State = new GenericBracketState(stateMachine, state);
                    return true;
                case '(':
                    stateMachine.State = new RoundParenthesisState(stateMachine, state);
                    return true;
                case '{':
                    stateMachine.State = new CurvedBracketState(stateMachine, state);
                    return true;
                case '\'':
                    stateMachine.State = new ApostropheBracketState(stateMachine, state);
                    return true;
                case '"':
                    stateMachine.State = new QuotesBracketState(stateMachine, state);
                    return true;
                default:
                    return false;
            }
        }
    }
}

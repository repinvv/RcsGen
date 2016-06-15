namespace RcsGen.SyntaxTree.States.BracketStates
{
    using System.Linq;

    internal class BracketStateFactory
    {
        private readonly StateMachine stateMachine;
        private readonly string[] allowed;
        private readonly IAccumulatingState state;

        public static readonly string[] AllBrackets = { "<", "(", "{", "\"", "'", "@", "[" };

        public BracketStateFactory(StateMachine stateMachine, IAccumulatingState state, params string[] allowed)
        {
            this.stateMachine = stateMachine;
            this.allowed = allowed;
            this.state = state;
        }

        public bool TryBracket(string token)
        {
            if (!allowed.Contains(token))
            {
                return false;
            }

            switch (token)
            {
                //case "@":
                //    stateMachine.State = new AtBracketState(stateMachine, state);
                //    return true;
                case "<":
                    stateMachine.State = new GenericBracketState(stateMachine, state);
                    return true;
                case "(":
                    stateMachine.State = new RoundParenthesisState(stateMachine, state);
                    return true;
                case "{":
                    stateMachine.State = new CurvedBracketState(stateMachine, state);
                    return true;
                case "'":
                    stateMachine.State = new ApostropheBracketState(stateMachine, state);
                    return true;
                case "\"":
                    stateMachine.State = new QuotesBracketState(stateMachine, state);
                    return true;
                case "[":
                    stateMachine.State = new SquareBracketState(stateMachine, state);
                    return true;
                default:
                    return false;
            }
        }
    }
}
